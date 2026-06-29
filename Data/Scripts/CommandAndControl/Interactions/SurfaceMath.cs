using VRageMath;

namespace AGS
{
    public static class SurfaceMath
    {
        public static Vector3D GetLinePlaneIntersection(
            Vector3D planePoint,
            Vector3D planeNormal,
            Vector3D linePoint,
            Vector3D lineDirection)
        {
            var normalizedDirection = Vector3D.Normalize(lineDirection);
            var planeDotLine = planeNormal.Dot(normalizedDirection);
            if (planeDotLine == 0 || planeDotLine >= 0)
            {
                return Vector3D.Zero;
            }

            var distance = (planeNormal.Dot(planePoint) - planeNormal.Dot(linePoint)) / planeDotLine;
            if (distance <= 0)
            {
                return Vector3D.Zero;
            }

            return linePoint + (normalizedDirection * distance);
        }

        public static Vector2 GetPointOnPlane(Vector3D point, Vector3D planePoint, Vector3D planeUp, Vector3D planeRight)
        {
            var x = planeRight.Dot(planePoint - point);
            var y = planeUp.Dot(planePoint - point);
            return new Vector2((float)x, (float)y);
        }

        public static Vector3D LocalToGlobal(Vector3D localPosition, MatrixD worldMatrix)
        {
            return Vector3D.Transform(localPosition, worldMatrix);
        }
    }
}
