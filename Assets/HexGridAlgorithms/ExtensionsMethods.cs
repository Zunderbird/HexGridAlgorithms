using System;

namespace Assets.HexGridAlgorithms
{
    public static class ExtensionsMethods
    {
        public static HexCoord ToHexCoord(this Point coord)
        {
            var q = (int)Math.Ceiling(coord.X - ((double)coord.Y / 2));
            var r = coord.Y;
            var s = -(q + coord.Y);
            return new HexCoord(q, r, s);
        }

        public static Point ToHexCoord(this HexCoord hexCoord)
        {
            var x = hexCoord.Q + hexCoord.R/2;
            var y = hexCoord.R;
           
            return new Point(x, y);
        }

        public static bool IsDigit(this char arg)
        {
            return ('0' <= arg && arg <= '9');
        }

        public static int ConvertToIntByLastSymbol(this string arg)
        {
            return arg.ConvertToIntByLastSymbol(int.MaxValue);
        }

        public static int ConvertToIntByLastSymbol(this string arg, int maxSizeNumber)
        {
            var number = 0;

            if (arg.Length <= 0) return number;

            var lastSymbol = arg[arg.Length - 1];

            if (!lastSymbol.IsDigit() || Convert.ToInt32(arg) > maxSizeNumber)
                arg = arg.Remove(arg.Length - 1);

            number = (arg.Length > 0) ? Convert.ToInt32(arg) : 0;

            return number;
        }
    }
}
