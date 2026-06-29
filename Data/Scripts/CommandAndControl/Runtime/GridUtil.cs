using System.Collections.Generic;
using Sandbox.ModAPI;
using VRage.Game.ModAPI;

namespace AGS
{
    public static class GridUtil
    {
        public static long GetConstructId(IMyCubeGrid grid, List<IMyCubeGrid> constructGrids)
        {
            if (grid == null)
            {
                return 0;
            }

            GetConstructGrids(grid, constructGrids);
            if (constructGrids.Count == 0)
            {
                return grid.EntityId;
            }

            var min = long.MaxValue;
            for (var i = 0; i < constructGrids.Count; i++)
            {
                if (constructGrids[i].EntityId < min)
                {
                    min = constructGrids[i].EntityId;
                }
            }

            return min;
        }

        public static void GetConstructGrids(IMyCubeGrid grid, List<IMyCubeGrid> constructGrids)
        {
            constructGrids.Clear();
            if (grid == null)
            {
                return;
            }

            MyAPIGateway.GridGroups.GetGroup(grid, GridLinkTypeEnum.Mechanical, constructGrids);
            if (constructGrids.Count == 0)
            {
                constructGrids.Add(grid);
            }
        }

        public static void GetConstructBlocks(List<IMyCubeGrid> grids, List<IMyCubeBlock> blocks)
        {
            blocks.Clear();
            if (grids == null)
            {
                return;
            }

            var slimBlocks = new List<IMySlimBlock>();
            for (var i = 0; i < grids.Count; i++)
            {
                var grid = grids[i];
                if (grid == null || grid.MarkedForClose || grid.Closed)
                {
                    continue;
                }

                slimBlocks.Clear();
                grid.GetBlocks(slimBlocks, slim => slim != null && slim.FatBlock != null);
                for (var j = 0; j < slimBlocks.Count; j++)
                {
                    var fat = slimBlocks[j].FatBlock as IMyCubeBlock;
                    if (fat != null && !fat.MarkedForClose && !fat.Closed)
                    {
                        blocks.Add(fat);
                    }
                }
            }
        }
    }
}
