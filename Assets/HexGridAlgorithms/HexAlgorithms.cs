using System;

namespace Assets.HexGridAlgorithms
{
    static class HexAlgorithms
    {
        public static int CalculateDistance(Vector3D firstHexPos, Vector3D secondHexPos)
        {
            var dx = Math.Abs(firstHexPos.X - secondHexPos.X);
            var dy = Math.Abs(firstHexPos.Y - secondHexPos.Y);
            var dz = Math.Abs(firstHexPos.Z - secondHexPos.Z);

            return Math.Max(dx, Math.Max(dy, dz));
        }
    }
}
