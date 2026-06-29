using System;
using System.Collections.Generic;
using VRageMath;

namespace AGS
{
    public sealed class ShipProjectedCell
    {
        public int U { get; set; }
        public int V { get; set; }
        public int Depth { get; set; }
        public int OwnerIndex { get; set; }
    }

    public sealed class ShipProjectedLine
    {
        public Vector2 A { get; set; }
        public Vector2 B { get; set; }
        public int Depth { get; set; }
        public int OwnerIndex { get; set; }
    }

    public sealed class ShipProjectionResult
    {
        public ShipProjectionResult()
        {
            VisibleCells = new Dictionary<long, ShipProjectedCell>();
            Seams = new List<ShipProjectedLine>();
            MinU = int.MaxValue;
            MinV = int.MaxValue;
            MaxU = int.MinValue;
            MaxV = int.MinValue;
            MinDepth = int.MaxValue;
            MaxDepth = int.MinValue;
        }

        public Dictionary<long, ShipProjectedCell> VisibleCells { get; private set; }
        public List<ShipProjectedLine> Seams { get; private set; }
        public int MinU { get; set; }
        public int MinV { get; set; }
        public int MaxU { get; set; }
        public int MaxV { get; set; }
        public int MinDepth { get; set; }
        public int MaxDepth { get; set; }

        public bool HasCells
        {
            get { return VisibleCells.Count > 0; }
        }
    }

    public static class ShipProjection
    {
        public static ShipProjectionResult Build(ShipVoxelModel ship, ShipView view, int selectedLayer)
        {
            var result = new ShipProjectionResult();
            if (ship == null || !ship.HasCells)
            {
                return result;
            }

            for (var i = 0; i < ship.Cells.Count; i++)
            {
                var cell = ship.Cells[i];
                int u, v, depth;
                ProjectCell(cell.X, cell.Y, cell.Z, view, out u, out v, out depth);
                if (depth > selectedLayer)
                {
                    continue;
                }

                var key = Pack(u, v);
                ShipProjectedCell existing;
                if (!result.VisibleCells.TryGetValue(key, out existing) || depth >= existing.Depth)
                {
                    result.VisibleCells[key] = new ShipProjectedCell
                    {
                        U = u,
                        V = v,
                        Depth = depth,
                        OwnerIndex = cell.OwnerIndex
                    };
                }
            }

            foreach (var pair in result.VisibleCells)
            {
                var cell = pair.Value;
                if (cell.U < result.MinU) result.MinU = cell.U;
                if (cell.V < result.MinV) result.MinV = cell.V;
                if (cell.U > result.MaxU) result.MaxU = cell.U;
                if (cell.V > result.MaxV) result.MaxV = cell.V;
                if (cell.Depth < result.MinDepth) result.MinDepth = cell.Depth;
                if (cell.Depth > result.MaxDepth) result.MaxDepth = cell.Depth;
            }

            if (!result.HasCells)
            {
                return result;
            }

            for (var i = 0; i < ship.Blocks.Count; i++)
            {
                var block = ship.Blocks[i];
                if (block == null || block.Cells.Count == 0)
                {
                    continue;
                }

                var blockDepth = GetVisibleBlockDepth(block, view, selectedLayer);
                if (blockDepth == int.MinValue)
                {
                    continue;
                }

                var seams = new List<ShipLinePrimitive>();
                ShipShapeLibrary.CollectProjectedSeams(block, view, seams);
                for (var j = 0; j < seams.Count; j++)
                {
                    var seam = seams[j];
                    if (!IsSegmentVisible(seam, block.Index, result.VisibleCells))
                    {
                        continue;
                    }

                    result.Seams.Add(new ShipProjectedLine
                    {
                        A = new Vector2(seam.AX, seam.AY),
                        B = new Vector2(seam.BX, seam.BY),
                        Depth = blockDepth,
                        OwnerIndex = block.Index
                    });
                }
            }

            return result;
        }

        public static void ProjectCell(int x, int y, int z, ShipView view, out int u, out int v, out int depth)
        {
            switch (view)
            {
                case ShipView.Bottom:
                    u = x;
                    v = z;
                    depth = y;
                    return;
                case ShipView.Left:
                    u = z;
                    v = -y;
                    depth = x;
                    return;
                case ShipView.Right:
                    u = -z;
                    v = -y;
                    depth = x;
                    return;
                case ShipView.Front:
                    u = x;
                    v = -y;
                    depth = z;
                    return;
                case ShipView.Back:
                    u = -x;
                    v = -y;
                    depth = z;
                    return;
                default:
                    u = x;
                    v = -z;
                    depth = y;
                    return;
            }
        }

        private static int GetVisibleBlockDepth(ShipBlockGeometry block, ShipView view, int selectedLayer)
        {
            var depth = int.MinValue;
            for (var i = 0; i < block.Cells.Count; i++)
            {
                int u, v, cellDepth;
                ProjectCell(block.Cells[i].X, block.Cells[i].Y, block.Cells[i].Z, view, out u, out v, out cellDepth);
                if (cellDepth > selectedLayer)
                {
                    continue;
                }
                if (cellDepth > depth)
                {
                    depth = cellDepth;
                }
            }
            return depth;
        }

        private static bool IsSegmentVisible(ShipLinePrimitive seam, int ownerIndex, Dictionary<long, ShipProjectedCell> visibleCells)
        {
            var edge = new Vector2(seam.BX - seam.AX, seam.BY - seam.AY);
            if (edge.LengthSquared() <= 0.0001f)
            {
                return false;
            }

            var midpoint = new Vector2((seam.AX + seam.BX) * 0.5f, (seam.AY + seam.BY) * 0.5f);
            var normal = new Vector2(edge.Y, -edge.X);
            if (normal.LengthSquared() <= 0.0001f)
            {
                return false;
            }

            normal.Normalize();
            const float epsilon = 0.22f;
            var sideA = ContainsOccupied(midpoint - (normal * epsilon), visibleCells);
            var sideB = ContainsOccupied(midpoint + (normal * epsilon), visibleCells);
            if (sideA == sideB)
            {
                return false;
            }

            for (var i = 1; i <= 5; i++)
            {
                var t = i / 6f;
                var point = new Vector2(
                    seam.AX + ((seam.BX - seam.AX) * t),
                    seam.AY + ((seam.BY - seam.AY) * t));
                var key = Pack((int)Math.Round(point.X), (int)Math.Round(point.Y));
                ShipProjectedCell cell;
                if (visibleCells.TryGetValue(key, out cell) && cell.OwnerIndex == ownerIndex)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool ContainsOccupied(Vector2 point, Dictionary<long, ShipProjectedCell> visibleCells)
        {
            return visibleCells.ContainsKey(Pack((int)Math.Floor(point.X + 0.5f), (int)Math.Floor(point.Y + 0.5f)));
        }

        private static long Pack(int x, int y)
        {
            return ((long)x << 32) ^ (uint)y;
        }
    }
}
