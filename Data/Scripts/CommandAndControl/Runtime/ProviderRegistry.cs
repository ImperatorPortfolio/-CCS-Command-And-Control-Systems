using System;
using System.Collections.Generic;
using Sandbox.ModAPI;
using VRage.Game.ModAPI;

namespace AGS
{
    public sealed class ProviderRegistry
    {
        private readonly List<ISystemProvider> _providers = new List<ISystemProvider>();
        private readonly List<IMyCubeGrid> _constructGrids = new List<IMyCubeGrid>();
        private readonly List<IMyCubeBlock> _blocks = new List<IMyCubeBlock>();
        private int _lastUpdateTick;

        public ProviderRegistry()
        {
            Register(new ControllerCoreProvider());
            Register(new PowerProvider());
            Register(new PropulsionProvider());
            Register(new NavigationProvider());
            Register(new SensorProvider());
            Register(new WeaponProvider());
            Register(new DefenseProvider());
            Register(new IntegrityProvider());
            Register(new DamageProvider());
            Register(new SecurityProvider());
            Register(new InventoryLogisticsProvider());
            Register(new AlertProvider());
        }

        public void Update(long constructId, IMyCubeBlock primaryController, ShipState state, int tick)
        {
            if (state == null)
            {
                return;
            }

            if (_lastUpdateTick != 0 && tick - _lastUpdateTick < 30)
            {
                return;
            }

            _lastUpdateTick = tick;
            state.Tick = tick;
            state.Providers.Clear();
            state.Alerts.Clear();
            state.Contacts.Clear();

            if (primaryController == null || primaryController.CubeGrid == null)
            {
                return;
            }

            GridUtil.GetConstructGrids(primaryController.CubeGrid, _constructGrids);
            GridUtil.GetConstructBlocks(_constructGrids, _blocks);
            var context = new ShipContext(constructId, primaryController, _constructGrids, _blocks);
            for (var i = 0; i < _providers.Count; i++)
            {
                _providers[i].Update(context, state);
            }
        }

        private void Register(ISystemProvider provider)
        {
            _providers.Add(provider);
        }
    }

    internal static class ProviderUtil
    {
        public static int CountBlocks<T>(List<IMyCubeBlock> blocks) where T : class
        {
            var count = 0;
            for (var i = 0; i < blocks.Count; i++)
            {
                if (blocks[i] is T)
                {
                    count++;
                }
            }

            return count;
        }

        public static int CountFunctional(List<IMyCubeBlock> blocks)
        {
            var count = 0;
            for (var i = 0; i < blocks.Count; i++)
            {
                if (blocks[i] != null && blocks[i].IsFunctional)
                {
                    count++;
                }
            }

            return count;
        }

        public static int CountSubtype(List<IMyCubeBlock> blocks, string pattern)
        {
            var count = 0;
            for (var i = 0; i < blocks.Count; i++)
            {
                var block = blocks[i];
                if (block == null)
                {
                    continue;
                }

                var subtype = block.BlockDefinition.SubtypeId;
                if (!string.IsNullOrEmpty(subtype) && subtype.IndexOf(pattern, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    count++;
                }
            }

            return count;
        }

        public static void AddSnapshot(ShipState state, string id, string title, SubsystemStatus status, string summary)
        {
            state.Providers.Add(new ProviderSnapshot(id, title, status, summary));
        }
    }
}
