using System.Collections.Generic;
using Sandbox.ModAPI;
using VRage.Game.ModAPI;
using VRage.ModAPI;

namespace AGS
{
    public sealed class GridRegistry
    {
        private readonly SurfaceMap _surfaceMap;
        private readonly AppRegistry _apps;
        private readonly Dictionary<long, GridRuntime> _runtimes = new Dictionary<long, GridRuntime>();
        private readonly Dictionary<string, long> _screenToConstruct = new Dictionary<string, long>();
        private readonly List<IMyCubeGrid> _constructGrids = new List<IMyCubeGrid>();
        private readonly HashSet<IMyEntity> _entities = new HashSet<IMyEntity>();
        private readonly List<IMySlimBlock> _slimBlocks = new List<IMySlimBlock>();
        private readonly HashSet<string> _controllerSubtypes = new HashSet<string> { ControllerIds.Small, ControllerIds.Large };
        private int _tick;

        public GridRegistry(SurfaceMap surfaceMap, AppRegistry apps)
        {
            _surfaceMap = surfaceMap;
            _apps = apps;
        }

        public void Update()
        {
            _tick++;
            if (_tick == 1 || _tick % 60 == 0)
            {
                RescanControllers();
            }

            foreach (var runtime in _runtimes.Values)
            {
                runtime.Update();
            }

            Prune();
        }

        public void RegisterScreen(IMyCubeBlock block, Sandbox.ModAPI.IMyTextSurface surface)
        {
            if (block == null || block.CubeGrid == null || surface == null)
            {
                return;
            }

            var constructId = GridUtil.GetConstructId(block.CubeGrid, _constructGrids);
            GetOrCreate(constructId).RegisterScreen(block, surface);
            _screenToConstruct[GetScreenKey(block, surface)] = constructId;
        }

        public void UnregisterScreen(IMyCubeBlock block, Sandbox.ModAPI.IMyTextSurface surface)
        {
            var runtime = FindRuntime(block, surface);
            runtime?.UnregisterScreen(block, surface);
            _screenToConstruct.Remove(GetScreenKey(block, surface));
        }

        public UiFrame GetFrame(IMyCubeBlock block, Sandbox.ModAPI.IMyTextSurface surface)
        {
            var runtime = FindRuntime(block, surface);
            return runtime != null ? runtime.GetFrame(block, surface) : UiFrame.CreateOffline();
        }

        public void SubmitIntent(IMyCubeBlock block, Sandbox.ModAPI.IMyTextSurface surface, UiIntent intent)
        {
            var runtime = FindRuntime(block, surface);
            runtime?.SubmitIntent(block, surface, intent);
        }

        public void Clear()
        {
            foreach (var runtime in _runtimes.Values)
            {
                runtime.Clear();
            }

            _runtimes.Clear();
            _screenToConstruct.Clear();
        }

        private void RescanControllers()
        {
            foreach (var runtime in _runtimes.Values)
            {
                runtime.BeginControllerScan();
            }

            _entities.Clear();
            MyAPIGateway.Entities.GetEntities(_entities, entity => entity is IMyCubeGrid);
            foreach (var entity in _entities)
            {
                var grid = entity as IMyCubeGrid;
                if (grid == null || grid.MarkedForClose || grid.Closed)
                {
                    continue;
                }

                _slimBlocks.Clear();
                grid.GetBlocks(_slimBlocks, slim => IsControllerBlock(slim.FatBlock as IMyCubeBlock));
                for (var j = 0; j < _slimBlocks.Count; j++)
                {
                    var block = _slimBlocks[j].FatBlock as IMyCubeBlock;
                    if (block == null || block.CubeGrid == null)
                    {
                        continue;
                    }

                    var constructId = GridUtil.GetConstructId(block.CubeGrid, _constructGrids);
                    GetOrCreate(constructId).RegisterController(block);
                }
            }

            foreach (var runtime in _runtimes.Values)
            {
                runtime.EndControllerScan();
            }
        }

        private void Prune()
        {
            var remove = new List<long>();
            foreach (var pair in _runtimes)
            {
                if (!pair.Value.HasControllers && pair.Value.ScreenCount == 0)
                {
                    pair.Value.Clear();
                    remove.Add(pair.Key);
                }
            }

            for (var i = 0; i < remove.Count; i++)
            {
                _runtimes.Remove(remove[i]);
            }
        }

        private GridRuntime GetOrCreate(long constructId)
        {
            GridRuntime runtime;
            if (!_runtimes.TryGetValue(constructId, out runtime))
            {
                runtime = new GridRuntime(constructId, _surfaceMap, _apps);
                _runtimes[constructId] = runtime;
            }

            return runtime;
        }

        private GridRuntime FindRuntime(IMyCubeBlock block, Sandbox.ModAPI.IMyTextSurface surface)
        {
            long constructId;
            if (_screenToConstruct.TryGetValue(GetScreenKey(block, surface), out constructId))
            {
                GridRuntime runtime;
                if (_runtimes.TryGetValue(constructId, out runtime))
                {
                    return runtime;
                }
            }

            if (block == null || block.CubeGrid == null)
            {
                return null;
            }

            return GetOrCreate(GridUtil.GetConstructId(block.CubeGrid, _constructGrids));
        }

        private bool IsControllerBlock(IMyCubeBlock block)
        {
            return block != null && _controllerSubtypes.Contains(block.BlockDefinition.SubtypeId);
        }

        private static string GetScreenKey(IMyCubeBlock block, Sandbox.ModAPI.IMyTextSurface surface)
        {
            if (block == null || surface == null)
            {
                return string.Empty;
            }

            var provider = block as Sandbox.ModAPI.IMyTextSurfaceProvider;
            var index = provider != null ? SurfaceUtil.GetSurfaceIndex(provider, surface) : -1;
            return block.EntityId + ":" + index;
        }
    }

    public static class ControllerIds
    {
        public const string Small = "ComputerControllerSmall";
        public const string Large = "ComputerControllerLarge";
    }
}
