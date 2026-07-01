using System;
using System.Collections.Generic;
using ProtoBuf;
using Sandbox.Game;
using Sandbox.ModAPI;
using VRage.Game.ModAPI;
using VRage.Utils;

namespace AGS
{
    // Wire message: a client asking the server to change its own input blacklist.
    [ProtoContract(UseProtoMembersOnly = true)]
    public sealed class BlacklistStateMessage
    {
        [ProtoMember(1)]
        public List<string> Controls;

        [ProtoMember(2)]
        public long PlayerId;

        [ProtoMember(3)]
        public bool Enabled;

        public BlacklistStateMessage() { }
    }

    // The per-player input blacklist is server-authoritative: MyVisualScriptLogicProvider
    // .SetPlayerInputBlacklistState only takes effect on the server, so a multiplayer
    // client calling it locally does nothing. This routes the request to the server, which
    // applies it (and the game syncs the result back). On singleplayer or on the server we
    // apply directly. Mirrors Adrian Lima's TouchScreenAPI NetworkBlacklistState.
    public sealed class InputBlacklistNetwork
    {
        private readonly ushort _channel;
        private bool _registered;

        public InputBlacklistNetwork(ushort channel)
        {
            _channel = channel;
        }

        public void Init()
        {
            // Only the server consumes blacklist requests from clients.
            if (MyAPIGateway.Multiplayer != null && MyAPIGateway.Multiplayer.IsServer)
            {
                MyAPIGateway.Multiplayer.RegisterSecureMessageHandler(_channel, HandleMessage);
                _registered = true;
            }
        }

        public void Dispose()
        {
            if (_registered && MyAPIGateway.Multiplayer != null)
            {
                MyAPIGateway.Multiplayer.UnregisterSecureMessageHandler(_channel, HandleMessage);
                _registered = false;
            }
        }

        public void SetPlayerInputBlacklistState(List<string> controls, long playerId, bool enabled)
        {
            if (controls == null || controls.Count == 0)
            {
                return;
            }

            // Singleplayer or server-side: apply directly, no round trip needed.
            if (MyAPIGateway.Multiplayer == null || !MyAPIGateway.Multiplayer.MultiplayerActive || MyAPIGateway.Multiplayer.IsServer)
            {
                ApplyBlacklistState(controls, playerId, enabled);
                return;
            }

            // Multiplayer client: ask the server to apply it for us.
            var message = new BlacklistStateMessage
            {
                Controls = controls,
                PlayerId = playerId,
                Enabled = enabled
            };

            var bytes = MyAPIGateway.Utilities.SerializeToBinary(message);
            MyAPIGateway.Multiplayer.SendMessageToServer(_channel, bytes);
        }

        private void HandleMessage(ushort handler, byte[] rawData, ulong senderSteamId, bool isFromServer)
        {
            try
            {
                // Server-only handler; never act on a message the server itself broadcast.
                if (isFromServer || MyAPIGateway.Multiplayer == null || !MyAPIGateway.Multiplayer.IsServer)
                {
                    return;
                }

                var message = MyAPIGateway.Utilities.SerializeFromBinary<BlacklistStateMessage>(rawData);
                if (message == null || message.Controls == null || message.Controls.Count == 0)
                {
                    return;
                }

                // A client may only change its own blacklist, never another player's.
                if (!IsSenderAuthorized(senderSteamId, message.PlayerId))
                {
                    return;
                }

                ApplyBlacklistState(message.Controls, message.PlayerId, message.Enabled);
            }
            catch (Exception e)
            {
                MyLog.Default.WriteLineAndConsole($"[CCS] InputBlacklistNetwork handler failed: {e.Message}\n{e.StackTrace}");
            }
        }

        // Confirms the sending Steam user actually owns the identity it is asking us to
        // modify, so a malicious client cannot blacklist another player's controls.
        private static bool IsSenderAuthorized(ulong senderSteamId, long identityId)
        {
            var players = new List<IMyPlayer>();
            MyAPIGateway.Players.GetPlayers(players, p => p != null && p.SteamUserId == senderSteamId);
            for (var i = 0; i < players.Count; i++)
            {
                if (players[i].IdentityId == identityId)
                {
                    return true;
                }
            }

            return false;
        }

        private static void ApplyBlacklistState(List<string> controls, long playerId, bool enabled)
        {
            for (var i = 0; i < controls.Count; i++)
            {
                MyVisualScriptLogicProvider.SetPlayerInputBlacklistState(controls[i], playerId, enabled);
            }
        }
    }
}
