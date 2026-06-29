using System;
using Sandbox.ModAPI;
using VRage.Game.Components;

namespace Imperator.CommandOS.SDK.Example
{
    /// <summary>
    /// Full integration skeleton. Replace the placeholder backend calls with calls into
    /// the weapon pack's real firing controller and resource-sink layer.
    /// Do not ship this example unchanged in production.
    /// </summary>
    [MySessionComponentDescriptor(MyUpdateOrder.AfterSimulation)]
    public sealed class WeaponPackCommandOsIntegration : MySessionComponentBase
    {
        private readonly CommandOsApi _api = new CommandOsApi();
        private bool _providerRegistered;

        public override void LoadData()
        {
            _api.Load();
        }

        public override void UpdateAfterSimulation()
        {
            if (_providerRegistered || !_api.IsReady)
                return;

            _api.RegisterWeaponProvider(
                "YourWeaponPack.v1",
                Supports,
                IsFiring,
                ApplyRateMultiplier,
                ApplyLockout,
                ApplyPowerDemand,
                GetHeatPerSecond);
            _providerRegistered = true;
        }

        protected override void UnloadData()
        {
            if (_providerRegistered)
                _api.UnregisterWeaponProvider("YourWeaponPack.v1");
            _api.Unload();
            _providerRegistered = false;
        }

        private static bool Supports(IMyTerminalBlock block)
        {
            // Replace this exact subtype check with the weapon pack's authoritative registry.
            return block != null && block.BlockDefinition.SubtypeName == "CompactAutocannon";
        }

        private static bool IsFiring(IMyTerminalBlock block)
        {
            // Replace with the weapon backend's accepted-shot or active-fire state.
            IMyUserControllableGun gun = block as IMyUserControllableGun;
            return gun != null && gun.IsShooting;
        }

        private static void ApplyRateMultiplier(IMyTerminalBlock block, float multiplier)
        {
            // Replace with a backend call that changes the weapon instance's fire interval.
            // Example intent: WeaponController.SetRateMultiplier(block.EntityId, multiplier);
        }

        private static void ApplyLockout(IMyTerminalBlock block, bool locked)
        {
            // Replace with a backend safety interlock. The weapon must reject fire while locked.
        }

        private static void ApplyPowerDemand(IMyTerminalBlock block, float megawatts)
        {
            // Replace with a backend resource-sink demand update. Do not drain batteries directly.
        }

        private static float GetHeatPerSecond(IMyTerminalBlock block)
        {
            // Return the weapon's calibrated heat generation while firing at 1.0x rate.
            return 8f;
        }
    }
}
