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

                // Cells are projected into the primary controller's local frame, but block
                // orientations are grid-relative. This maps a grid direction into that same
                // local frame so slope geometry lines up with the cells (otherwise slopes
                // point wrong whenever the controller isn't axis-aligned with the grid).
                var gridToLocal = grid.WorldMatrix * worldToLocal;

                slimBlocks.Clear();
                grid.GetBlocks(slimBlocks, slim => slim != null);
                for (var j = 0; j < slimBlocks.Count; j++)
                {
                    var slim = slimBlocks[j];
                    var fat = slim.FatBlock as IMyCubeBlock;
                    var subtypeName = GetSubtypeName(slim, fat);
                    var bounds = GetLocalBounds(grid, slim.Min, slim.Max, worldToLocal, scale);

                    if (!IsArmorSubtype(subtypeName))
                    {
                        // Non-armour: keep only recognised specialty systems, as a single
                        // icon marker at the block centre. These do not contribute hull cells.
                        ShipDeviceCategory category;
                        if (TryResolveDevice(slim, out category))
                        {
                            model.AddDevice(new ShipDeviceMarker(
                                category,
                                (bounds.Min.X + bounds.Max.X) * 0.5f,
                                (bounds.Min.Y + bounds.Max.Y) * 0.5f,
                                (bounds.Min.Z + bounds.Max.Z) * 0.5f));
                        }
                        continue;
                    }

                    var shapeId = ResolveShapeId(subtypeName);
                    var forwardGrid = fat != null ? (int)fat.Orientation.Forward : (int)slim.Orientation.Forward;
                    var upGrid = fat != null ? (int)fat.Orientation.Up : (int)slim.Orientation.Up;
                    var forward = LocalDirection(forwardGrid, gridToLocal);
                    var up = LocalDirection(upGrid, gridToLocal);
                    var right = ResolveRight(forward, up);
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

        // Classifies a non-armour block into a specialty category by its object-builder type
        // (e.g. "MyObjectBuilder_Thrust"). Returns false for blocks we don't mark (lights,
        // conveyors, passages, etc.) so the map stays readable.
        private static bool TryResolveDevice(IMySlimBlock slim, out ShipDeviceCategory category)
        {
            category = ShipDeviceCategory.Other;
            var typeId = slim != null ? slim.BlockDefinition.Id.TypeId.ToString() : string.Empty;
            if (string.IsNullOrEmpty(typeId))
            {
                return false;
            }

            if (Has(typeId, "Thrust")) { category = ShipDeviceCategory.Thruster; return true; }
            if (Has(typeId, "Reactor")) { category = ShipDeviceCategory.Reactor; return true; }
            if (Has(typeId, "Battery")) { category = ShipDeviceCategory.Battery; return true; }
            if (Has(typeId, "GasTank") || Has(typeId, "OxygenTank")) { category = ShipDeviceCategory.Tank; return true; }
            if (Has(typeId, "Cockpit") || Has(typeId, "RemoteControl") || Has(typeId, "Cryo") || Has(typeId, "ShipController")) { category = ShipDeviceCategory.Controller; return true; }
            if (Has(typeId, "Turret") || Has(typeId, "Gatling") || Has(typeId, "Missile") || Has(typeId, "Launcher") || Has(typeId, "Gun")) { category = ShipDeviceCategory.Weapon; return true; }
            if (Has(typeId, "CargoContainer")) { category = ShipDeviceCategory.Cargo; return true; }
            if (Has(typeId, "Gyro")) { category = ShipDeviceCategory.Gyro; return true; }
            return false;
        }

        private static bool Has(string value, string token)
        {
            return value.IndexOf(token, StringComparison.OrdinalIgnoreCase) >= 0;
        }
        public static ShipShapeId ResolveShapeId(string subtypeName)
        {
            // Scope: only the basic 1x1 large armour slope (light + heavy) is drawn as a
            // sloped face for now. Every other armour shape (corners, 2x1, variants) renders
            // as its voxel cube until its own template is added, so nothing is forced into
            // the wrong shape.
            if (string.Equals(subtypeName, "LargeBlockArmorSlope", StringComparison.OrdinalIgnoreCase)
                || string.Equals(subtypeName, "LargeHeavyBlockArmorSlope", StringComparison.OrdinalIgnoreCase))
            {
                return ShipShapeId.Slope;
            }

            if (string.Equals(subtypeName, "LargeBlockArmorSlope2Base", StringComparison.OrdinalIgnoreCase)
                || string.Equals(subtypeName, "LargeHeavyBlockArmorSlope2Base", StringComparison.OrdinalIgnoreCase))
            {
                return ShipShapeId.Slope2x1;
            }

            if (string.Equals(subtypeName, "LargeBlockArmorSlope2Tip", StringComparison.OrdinalIgnoreCase)
                || string.Equals(subtypeName, "LargeHeavyBlockArmorSlope2Tip", StringComparison.OrdinalIgnoreCase))
            {
                return ShipShapeId.Slope2x1Tip;
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

        // Re-expresses a grid-relative Base6 direction in the controller-local frame the
        // cells live in, then snaps back to the nearest axis direction.
        private static int LocalDirection(int gridDirection, MatrixD gridToLocal)
        {
            var v = Base6Directions.GetIntVector((Base6Directions.Direction)gridDirection);
            var local = Vector3D.TransformNormal(new Vector3D(v.X, v.Y, v.Z), gridToLocal);
            return (int)Base6Directions.GetClosestDirection(new Vector3((float)local.X, (float)local.Y, (float)local.Z));
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
