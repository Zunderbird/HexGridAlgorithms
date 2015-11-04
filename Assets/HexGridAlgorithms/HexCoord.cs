
using Newtonsoft.Json;

namespace Assets.HexGridAlgorithms
{
    public struct HexCoord
    {
        public int Q { get; set; }
        public int R { get; set; }
        public int S { get; set; }

        public HexCoord(int q, int r, int s)
        {
            Q = q;
            R = r;
            S = s;
        }

        public HexCoord(HexCoord previousHexCoord)
        {
            Q = previousHexCoord.Q;
            R = previousHexCoord.R;
            S = previousHexCoord.S;
        }

        public void Offset(int dq, int dr, int ds)
        {
            Q += dq;
            R += dr;
            S += ds;
        }

        public static HexCoord operator +(HexCoord left, HexCoord right)
        {
            return new HexCoord(
                left.Q + right.Q,
                left.R + right.R,
                left.S + right.S);
        }

        public static HexCoord operator -(HexCoord left, HexCoord right)
        {
            return new HexCoord(
                left.Q - right.Q,
                left.R - right.R,
                left.S - right.S);
        }

        public static HexCoord operator *(HexCoord left, HexCoord right)
        {
            return new HexCoord(
                left.Q * right.Q,
                left.R * right.R,
                left.S * right.S);
        }

        public static HexCoord operator /(HexCoord left, HexCoord right)
        {
            return new HexCoord(
                left.Q / right.Q,
                left.R / right.R,
                left.S / right.S);
        }

        public static bool operator ==(HexCoord left, HexCoord right)
        {
            return left.Q == right.Q
                && left.R == right.R
                && left.S == right.S;
        }

        public static bool operator !=(HexCoord left, HexCoord right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is HexCoord)) return false;
            var comp = (HexCoord)obj;

            return comp.Q == Q
                && comp.R == R
                && comp.S == S;
        }

        public override int GetHashCode()
        {
            return Q ^ R ^ S;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);      
        }
    }
}
