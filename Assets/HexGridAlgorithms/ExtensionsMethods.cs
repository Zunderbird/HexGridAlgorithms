using UnityEngine;

namespace Assets.HexGridAlgorithms
{
    public static class ExtensionsMethods
    {
        public static Vector3D ToVector3D(this Vector3 vector)
        {
            return new Vector3D((int)vector.x, (int)vector.y, (int)vector.z);
        }

        public static Vector3 ToVector3(this Vector3D vector)
        {
            return new Vector3(vector.X, vector.Y, vector.Z);
        }

        public static bool IsDigit(this char arg)
        {
            return ('0' <= arg && arg <= '9');
        }
    }
}
