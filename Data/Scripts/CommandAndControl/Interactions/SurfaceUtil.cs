using Sandbox.ModAPI;
using VRage.Game.ModAPI;
using VRageMath;

namespace AGS
{
    public static class SurfaceUtil
    {
        public static int GetSurfaceIndex(Sandbox.ModAPI.IMyTextSurfaceProvider provider, Sandbox.ModAPI.IMyTextSurface surface)
        {
            for (var i = 0; i < provider.SurfaceCount; i++)
            {
                if (provider.GetSurface(i) == surface)
                {
                    return i;
                }
            }

            return -1;
        }

        public static Vector2 RotateScreenCoord(Vector2 coord, int rotation)
        {
            switch (rotation)
            {
                case 1:
                    return new Vector2(coord.Y, 1f - coord.X);
                case 2:
                    return new Vector2(1f - coord.X, 1f - coord.Y);
                case 3:
                    return new Vector2(1f - coord.Y, coord.X);
                default:
                    return coord;
            }
        }

        public static int GetRotation(IMyCubeBlock block)
        {
            // v1 keeps rotation handling conservative to avoid terminal-property API differences.
            // Most supported screens in the default mapping are front-facing and work correctly at 0.
            return 0;
        }

        public static float GetInteractiveDistance(SurfaceCoords coords)
        {
            var inches = Vector3.Distance(coords.TopLeft, coords.BottomRight) * 1.75f;
            var min = inches < 1f ? 2.5f : 5f;
            return MathHelper.Clamp(inches, min, 15f);
        }
    }
}
