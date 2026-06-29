using System;
using System.Collections.Generic;
using VRageMath;

namespace AGS
{
    public static class ShipShapeLibrary
    {
        public static void CollectProjectedSeams(ShipBlockGeometry block, ShipView view, List<ShipLinePrimitive> result)
        {
            if (block == null || result == null)
            {
                return;
            }

            var center = ProjectPoint(block.CenterX, block.CenterY, block.CenterZ, view);
            var rightAxis = ProjectDirection(block.Right, view);
            var forwardAxis = ProjectDirection(block.Forward, view);
            var upAxis = ProjectDirection(block.Up, view);

            var rightHalf = ScaleProjectedAxis(rightAxis, GetAxisSpan(block, block.Right) * 0.5f);
            var forwardHalf = ScaleProjectedAxis(forwardAxis, GetAxisSpan(block, block.Forward) * 0.5f);
            var upHalf = ScaleProjectedAxis(upAxis, GetAxisSpan(block, block.Up) * 0.5f);
            var planView = rightHalf.LengthSquared() > 0.001f && forwardHalf.LengthSquared() > 0.001f;

            switch (block.ShapeId)
            {
                case ShipShapeId.Slope:
                    AddSlope(center, rightHalf, forwardHalf, upHalf, planView, result);
                    break;
                case ShipShapeId.Corner:
                    AddCorner(center, rightHalf, forwardHalf, upHalf, planView, false, result);
                    break;
                case ShipShapeId.InvertedCorner:
                    AddCorner(center, rightHalf, forwardHalf, upHalf, planView, true, result);
                    break;
                case ShipShapeId.Slope2x1:
                    AddLongSlope(center, rightHalf, forwardHalf, upHalf, planView, false, result);
                    break;
                case ShipShapeId.Slope2x1Tip:
                    AddLongSlope(center, rightHalf, forwardHalf, upHalf, planView, true, result);
                    break;
            }
        }

        private static void AddSlope(Vector2 center, Vector2 rightHalf, Vector2 forwardHalf, Vector2 upHalf, bool planView, List<ShipLinePrimitive> result)
        {
            if (planView)
            {
                var a = center + forwardHalf - rightHalf;
                var b = center - forwardHalf + rightHalf;
                result.Add(new ShipLinePrimitive(a.X, a.Y, b.X, b.Y));
                return;
            }

            AddVerticalDiagonal(center, DominantHalf(rightHalf, forwardHalf), upHalf, result);
        }

        private static void AddCorner(Vector2 center, Vector2 rightHalf, Vector2 forwardHalf, Vector2 upHalf, bool planView, bool inverted, List<ShipLinePrimitive> result)
        {
            if (planView)
            {
                if (inverted)
                {
                    var a = center - forwardHalf;
                    var b = center + rightHalf;
                    result.Add(new ShipLinePrimitive(a.X, a.Y, b.X, b.Y));
                }
                else
                {
                    var a = center - rightHalf;
                    var b = center - forwardHalf;
                    result.Add(new ShipLinePrimitive(a.X, a.Y, b.X, b.Y));
                }
                return;
            }

            AddVerticalDiagonal(center, DominantHalf(rightHalf, forwardHalf), upHalf, result);
        }

        private static void AddLongSlope(Vector2 center, Vector2 rightHalf, Vector2 forwardHalf, Vector2 upHalf, bool planView, bool tip, List<ShipLinePrimitive> result)
        {
            if (planView)
            {
                var rightIsMajor = rightHalf.LengthSquared() >= forwardHalf.LengthSquared();
                var majorHalf = rightIsMajor ? rightHalf : forwardHalf;
                var minorHalf = rightIsMajor ? forwardHalf : rightHalf;
                var a = center + majorHalf - minorHalf;
                var b = center - majorHalf + minorHalf;
                result.Add(new ShipLinePrimitive(a.X, a.Y, b.X, b.Y));
                if (tip)
                {
                    var tipStart = center + majorHalf;
                    var tipEnd = center + majorHalf - (minorHalf * 0.8f);
                    result.Add(new ShipLinePrimitive(tipStart.X, tipStart.Y, tipEnd.X, tipEnd.Y));
                }
                return;
            }

            AddVerticalDiagonal(center, DominantHalf(rightHalf, forwardHalf), upHalf, result);
        }

        private static void AddVerticalDiagonal(Vector2 center, Vector2 horizontalHalf, Vector2 upHalf, List<ShipLinePrimitive> result)
        {
            if (horizontalHalf.LengthSquared() <= 0.001f || upHalf.LengthSquared() <= 0.001f)
            {
                return;
            }

            var a = center - horizontalHalf + upHalf;
            var b = center + horizontalHalf - upHalf;
            result.Add(new ShipLinePrimitive(a.X, a.Y, b.X, b.Y));
        }

        private static float GetAxisSpan(ShipBlockGeometry block, int direction)
        {
            var axis = Base6Directions.GetIntVector((Base6Directions.Direction)direction);
            var spanX = block.MaxX - block.MinX + 1;
            var spanY = block.MaxY - block.MinY + 1;
            var spanZ = block.MaxZ - block.MinZ + 1;
            return (Math.Abs(axis.X) * spanX) + (Math.Abs(axis.Y) * spanY) + (Math.Abs(axis.Z) * spanZ);
        }

        private static Vector2 ScaleProjectedAxis(Vector2 axis, float halfSpan)
        {
            if (axis.LengthSquared() <= 0.0001f || halfSpan <= 0.0001f)
            {
                return Vector2.Zero;
            }

            axis.Normalize();
            return axis * halfSpan;
        }

        private static Vector2 DominantHalf(Vector2 a, Vector2 b)
        {
            return a.LengthSquared() >= b.LengthSquared() ? a : b;
        }

        private static Vector2 ProjectPoint(float x, float y, float z, ShipView view)
        {
            switch (view)
            {
                case ShipView.Bottom: return new Vector2(x, z);
                case ShipView.Left: return new Vector2(z, -y);
                case ShipView.Right: return new Vector2(-z, -y);
                case ShipView.Front: return new Vector2(x, -y);
                case ShipView.Back: return new Vector2(-x, -y);
                default: return new Vector2(x, -z);
            }
        }

        private static Vector2 ProjectDirection(int direction, ShipView view)
        {
            var axis = Base6Directions.GetIntVector((Base6Directions.Direction)direction);
            switch (view)
            {
                case ShipView.Bottom: return new Vector2(axis.X, axis.Z);
                case ShipView.Left: return new Vector2(axis.Z, -axis.Y);
                case ShipView.Right: return new Vector2(-axis.Z, -axis.Y);
                case ShipView.Front: return new Vector2(axis.X, -axis.Y);
                case ShipView.Back: return new Vector2(-axis.X, -axis.Y);
                default: return new Vector2(axis.X, -axis.Z);
            }
        }
    }
}
