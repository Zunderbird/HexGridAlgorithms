
namespace Assets.HexGridAlgorithms
{
    public struct Vector3D
    {
        public static readonly Vector3D Empty = new Vector3D();

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Vector3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3D(Vector3D previousVector3D)
        {
            X = previousVector3D.X;
            Y = previousVector3D.Y;
            Z = previousVector3D.Z;
        }

        public bool IsEmpty
        {
            get
            {
                return X == 0
                    && Y == 0
                    && Z == 0;
            }
        }

        public void Offset(int dx, int dy, int dz)
        {
            X += dx;
            Y += dy;
            Z += dz;
        }

        public static Vector3D operator +(Vector3D left, Vector3D right)
        {
            return new Vector3D
                (
                left.X + right.X,
                left.Y + right.Y,
                left.Z + right.Z);
        }

        public static Vector3D operator -(Vector3D left, Vector3D right)
        {
            return new Vector3D
                (
                left.X - right.X,
                left.Y - right.Y,
                left.Z - right.Z);
        }

        public static Vector3D operator *(Vector3D left, Vector3D right)
        {
            return new Vector3D(
                left.X * right.X,
                left.Y * right.Y,
                left.Z * right.Z);
        }

        public static Vector3D operator /(Vector3D left, Vector3D right)
        {
            return new Vector3D(
                left.X / right.X,
                left.Y / right.Y,
                left.Z / right.Z);
        }

        public static bool operator ==(Vector3D left, Vector3D right)
        {
            return left.X == right.X
                && left.Y == right.Y
                && left.Z == right.Z;
        }

        public static bool operator !=(Vector3D left, Vector3D right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector3D)) return false;
            var comp = (Vector3D)obj;

            return comp.X == X
                && comp.Y == Y
                && comp.Z == Z;
        }

        public override int GetHashCode()
        {
            return X ^ Y ^ Z;
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ", " + Z + ")";
        }
    }
}
