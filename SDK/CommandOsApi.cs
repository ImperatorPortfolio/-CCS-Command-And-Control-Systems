using System;
using System.Collections.Generic;
using Sandbox.ModAPI;

namespace Imperator.CommandOS.SDK
{
    /// <summary>
    /// Copy this complete file into an integrating weapon mod.
    /// Instantiate once per game instance, call Load during session LoadData,
    /// register the provider after IsReady becomes true, and call Unload on shutdown.
    /// </summary>
    public sealed class CommandOsApi
    {
        private const long Channel = 784312667104L;
        private const string Request = "CommandOS.Api.Request.v1";

        private Func<string> _version;
        private Action<
            string,
            Func<IMyTerminalBlock, bool>,
            Func<IMyTerminalBlock, bool>,
            Action<IMyTerminalBlock, float>,
            Action<IMyTerminalBlock, bool>,
            Action<IMyTerminalBlock, float>,
            Func<IMyTerminalBlock, float>> _registerWeaponProvider;
        private Action<string> _unregisterWeaponProvider;
        private bool _registered;

        public bool IsReady { get; private set; }
        public string Version { get { return _version == null ? string.Empty : _version(); } }

        public void Load()
        {
            if (!_registered)
            {
                MyAPIGateway.Utilities.RegisterMessageHandler(Channel, HandleMessage);
                _registered = true;
            }
            MyAPIGateway.Utilities.SendModMessage(Channel, Request);
        }

        public void Unload()
        {
            if (_registered)
                MyAPIGateway.Utilities.UnregisterMessageHandler(Channel, HandleMessage);
            _registered = false;
            IsReady = false;
            _version = null;
            _registerWeaponProvider = null;
            _unregisterWeaponProvider = null;
        }

        public void RegisterWeaponProvider(
            string providerId,
            Func<IMyTerminalBlock, bool> supports,
            Func<IMyTerminalBlock, bool> isFiring,
            Action<IMyTerminalBlock, float> applyRateMultiplier,
            Action<IMyTerminalBlock, bool> applyLockout,
            Action<IMyTerminalBlock, float> applyPowerDemandMW,
            Func<IMyTerminalBlock, float> getHeatPerSecond)
        {
            if (!IsReady)
                throw new InvalidOperationException("CommandOS API is not ready.");
            _registerWeaponProvider(
                providerId,
                supports,
                isFiring,
                applyRateMultiplier,
                applyLockout,
                applyPowerDemandMW,
                getHeatPerSecond);
        }

        public void UnregisterWeaponProvider(string providerId)
        {
            if (_unregisterWeaponProvider != null)
                _unregisterWeaponProvider(providerId);
        }

        private void HandleMessage(object message)
        {
            if (message is string)
                return;

            IReadOnlyDictionary<string, Delegate> endpoints = message as IReadOnlyDictionary<string, Delegate>;
            if (endpoints == null)
                return;

            _version = Get<Func<string>>(endpoints, "Version");
            _registerWeaponProvider = Get<Action<
                string,
                Func<IMyTerminalBlock, bool>,
                Func<IMyTerminalBlock, bool>,
                Action<IMyTerminalBlock, float>,
                Action<IMyTerminalBlock, bool>,
                Action<IMyTerminalBlock, float>,
                Func<IMyTerminalBlock, float>>>(endpoints, "RegisterWeaponProvider");
            _unregisterWeaponProvider = Get<Action<string>>(endpoints, "UnregisterWeaponProvider");
            IsReady = _version != null && _registerWeaponProvider != null && _unregisterWeaponProvider != null;
        }

        private static T Get<T>(IReadOnlyDictionary<string, Delegate> endpoints, string name) where T : class
        {
            Delegate value;
            if (!endpoints.TryGetValue(name, out value))
                return null;
            return value as T;
        }
    }
}
