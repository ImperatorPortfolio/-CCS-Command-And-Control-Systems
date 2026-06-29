using VRageMath;

namespace AGS
{
    public struct SurfaceCoords
    {
        public readonly string SubtypeId;
        public readonly int Index;
        public readonly Vector3 TopLeft;
        public readonly Vector3 BottomLeft;
        public readonly Vector3 BottomRight;

        public SurfaceCoords(string subtypeId, int index, Vector3 topLeft, Vector3 bottomLeft, Vector3 bottomRight)
        {
            SubtypeId = subtypeId;
            Index = index;
            TopLeft = topLeft;
            BottomLeft = bottomLeft;
            BottomRight = bottomRight;
        }

        public Vector3D GetNormal(MatrixD worldMatrix)
        {
            var a = SurfaceMath.LocalToGlobal(TopLeft, worldMatrix);
            var b = SurfaceMath.LocalToGlobal(BottomLeft, worldMatrix);
            var c = SurfaceMath.LocalToGlobal(BottomRight, worldMatrix);
            return Vector3D.Normalize(Vector3D.Cross(b - a, c - a));
        }
    }
}
