using System;
using System.Collections.Generic;
using Sandbox.ModAPI;
using VRage.Game.ModAPI;
using VRageMath;

namespace AGS
{
    public static class ShipGeometryBuilder
    {
        public static ShipVoxelModel Build(List<IMyCubeGrid> grids, MatrixD worldToLocal, double gridSize)
        {
            var model = new ShipVoxelModel();
            var occupied = new HashSet<long>();
            var slimBlocks = new List<IMySlimBlock>();
            var scale = gridSize <= 0.001 ? 2.5 : gridSize;

            for (var i = 0; i < grids.Count; i++)
            {
                var grid = grids[i];
                if (grid == null || grid.MarkedForClose || grid.Closed)
                {
                    continue;
                }

                slimBlocks.Clear();
                grid.GetBlocks(slimBlocks, slim => slim != null);
                for (var j = 0; j < slimBlocks.Count; j++)
                {
                    var slim = slimBlocks[j];
                    var fat = slim.FatBlock as IMyCubeBlock;
                    var subtypeName = GetSubtypeName(slim, fat);
                    if (!IsArmorSubtype(subtypeName))
                    {
                        continue;
                    }

                    var shapeId = ResolveShapeId(subtypeName);
                    var forward = fat != null ? (int)fat.Orientation.Forward : (int)slim.Orientation.Forward;
                    var up = fat != null ? (int)fat.Orientation.Up : (int)slim.Orientation.Up;
                    var right = ResolveRight(forward, up);
                    var bounds = GetLocalBounds(grid, slim.Min, slim.Max, worldToLocal, scale);
                    var block = new ShipBlockGeometry(model.Blocks.Count, subtypeName, shapeId, bounds.Min.X, bounds.Min.Y, bounds.Min.Z, bounds.Max.X, bounds.Max.Y, bounds.Max.Z, forward, up, right);
                    model.AddBlock(block);

                    for (var x = bounds.Min.X; x <= bounds.Max.X; x++)
                    {
                        for (var y = bounds.Min.Y; y <= bounds.Max.Y; y++)
                        {
                            for (var z = bounds.Min.Z; z <= bounds.Max.Z; z++)
                            {
                                var packed = Pack(x, y, z);
                                if (!occupied.Add(packed))
                                {
                                    continue;
                                }

                                var cell = model.AddCell(x, y, z, block.Index);
                                block.AddCell(cell);
                            }
                        }
                    }
                }
            }

            return model;
        }

        private static bool IsArmorSubtype(string subtypeName)
        {
            var lowered = (subtypeName ?? string.Empty).ToLowerInvariant();
            return lowered.Contains("armor") || lowered.Contains("armour");
        }
        public static ShipShapeId ResolveShapeId(string subtypeName)
        {
            var lowered = (subtypeName ?? string.Empty).ToLowerInvariant();
            if (lowered.Contains("2x1") && lowered.Contains("tip"))
            {
                return ShipShapeId.Slope2x1Tip;
            }
            if (lowered.Contains("2x1") && lowered.Contains("slope"))
            {
                return ShipShapeId.Slope2x1;
            }
            if (lowered.Contains("invcorner") || lowered.Contains("invertedcorner"))
            {
                return ShipShapeId.InvertedCorner;
            }
            if (lowered.Contains("corner"))
            {
                return ShipShapeId.Corner;
            }
            if (lowered.Contains("slope"))
            {
                return ShipShapeId.Slope;
            }
            return ShipShapeId.Cube;
        }

        private static BoundingBoxI GetLocalBounds(IMyCubeGrid grid, Vector3I min, Vector3I max, MatrixD worldToLocal, double scale)
        {
            var minLocal = ProjectCell(grid, min, worldToLocal, scale);
            var maxLocal = ProjectCell(grid, max, worldToLocal, scale);
            return new BoundingBoxI(Vector3I.Min(minLocal, maxLocal), Vector3I.Max(minLocal, maxLocal));
        }

        private static Vector3I ProjectCell(IMyCubeGrid grid, Vector3I cell, MatrixD worldToLocal, double scale)
        {
            var world = grid.GridIntegerToWorld(cell);
            var local = Vector3D.Transform(world, worldToLocal);
            return new Vector3I((int)Math.Round(local.X / scale), (int)Math.Round(local.Y / scale), (int)Math.Round(local.Z / scale));
        }

        private static string GetSubtypeName(IMySlimBlock slim, IMyCubeBlock fat)
        {
            if (fat != null)
            {
                return fat.BlockDefinition.SubtypeId ?? string.Empty;
            }

            return slim != null ? slim.BlockDefinition.Id.SubtypeName ?? string.Empty : string.Empty;
        }

        private static int ResolveRight(int forward, int up)
        {
            var forwardDir = Base6Directions.GetIntVector((Base6Directions.Direction)forward);
            var upDir = Base6Directions.GetIntVector((Base6Directions.Direction)up);
            Vector3I rightDir;
            Vector3I.Cross(ref upDir, ref forwardDir, out rightDir);
            return (int)Base6Directions.GetDirection(rightDir);
        }

        private static long Pack(int x, int y, int z)
        {
            var packedX = ((long)(x & 0x1FFFFF)) << 42;
            var packedY = ((long)(y & 0x1FFFFF)) << 21;
            var packedZ = (long)(z & 0x1FFFFF);
            return packedX | packedY | packedZ;
        }
    }
}
