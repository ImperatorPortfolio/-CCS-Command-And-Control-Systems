using System.Collections.Generic;
using Sandbox.Game;
using Sandbox.ModAPI;
using VRage.Game;
using VRage.Game.Components;

namespace AGS
{
    [MySessionComponentDescriptor(MyUpdateOrder.BeforeSimulation)]
    public sealed class Session : MySessionComponentBase
    {
        // Keep the controls blacklisted briefly after the pointer leaves a screen so a
        // glancing look-away does not flicker the suppression on and off.
        private const int ReleaseDelayTicks = 30;

        // Secure-message channel for routing blacklist changes to the server. Must be
        // unique across the mods loaded on the server.
        private const ushort BlacklistNetworkChannel = 0xCC51;

        private bool _useBlocked;
        private int _releaseDelay;
        private InputBlacklistNetwork _blacklistNetwork;
        private List<string> _suppressedControls;

        public static Program Program { get; private set; }
        public static MyObjectBuilder_Checkpoint.ModItem? ModItem { get; private set; }

        public override void LoadData()
        {
            ModItem = ModContext.ModItem;
            _blacklistNetwork = new InputBlacklistNetwork(BlacklistNetworkChannel);
            _blacklistNetwork.Init();
            Program = new Program();
            Program.Load();
        }

        public override void UpdateBeforeSimulation()
        {
            if (MyAPIGateway.Session == null)
            {
                return;
            }

            Program?.Update();
            UpdateInputSuppression();
        }

        protected override void UnloadData()
        {
            if (_useBlocked)
            {
                SetUseBlocked(false);
                _useBlocked = false;
            }

            _blacklistNetwork?.Dispose();
            _blacklistNetwork = null;

            Program?.Unload();
            Program = null;
            ModItem = null;
        }

        // Suppresses the vanilla left/right click (which opens the block terminal) while
        // the local player is aimed at one of our screens, so clicks reach the LCD UI
        // instead. Mirrors Adrian Lima's TouchScreenAPI approach.
        private void UpdateInputSuppression()
        {
            if (MyAPIGateway.Utilities == null || MyAPIGateway.Utilities.IsDedicated)
            {
                return;
            }

            var engaged = Program != null && Program.IsPointerEngaged();
            if (engaged)
            {
                _releaseDelay = ReleaseDelayTicks;
            }
            else if (_releaseDelay > 0)
            {
                _releaseDelay--;
            }

            var shouldBlock = engaged || _releaseDelay > 0;
            if (shouldBlock == _useBlocked)
            {
                return;
            }

            SetUseBlocked(shouldBlock);
            _useBlocked = shouldBlock;
        }

        private void SetUseBlocked(bool blocked)
        {
            var player = MyAPIGateway.Session != null ? MyAPIGateway.Session.Player : null;
            if (player == null || _blacklistNetwork == null)
            {
                return;
            }

            // The third parameter is "is the control enabled" — false blacklists (blocks)
            // the control, true allows it. So to block clicks we pass !blocked. Only the
            // primary/secondary tool actions open the LCD's control panel; USE is left
            // alone (matches Adrian Lima's TouchScreenAPI). The blacklist is server-owned,
            // so this goes through the network wrapper to work on MP clients and DS.
            _blacklistNetwork.SetPlayerInputBlacklistState(SuppressedControls, player.IdentityId, !blocked);
        }

        private List<string> SuppressedControls
        {
            get
            {
                if (_suppressedControls == null)
                {
                    _suppressedControls = new List<string>(2)
                    {
                        MyControlsSpace.PRIMARY_TOOL_ACTION.String,
                        MyControlsSpace.SECONDARY_TOOL_ACTION.String
                    };
                }

                return _suppressedControls;
            }
        }
    }
}
