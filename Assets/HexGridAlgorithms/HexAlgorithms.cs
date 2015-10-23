using System;

namespace Assets.HexGridAlgorithms
{
    static class HexAlgorithms
    {
        public static int CalculateDistance(HexCoord firstHexPos, HexCoord secondHexPos)
        {
            var dx = Math.Abs(firstHexPos.Q - secondHexPos.Q);
            var dy = Math.Abs(firstHexPos.R - secondHexPos.R);
            var dz = Math.Abs(firstHexPos.S - secondHexPos.S);

            return Math.Max(dx, Math.Max(dy, dz));

        }
    }
}
