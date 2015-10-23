using System;

namespace Assets.HexGridAlgorithms
{
    public class HexEventArgs: EventArgs
    {
        public TerrainTypes HexType { get; set; }

        public HexCoord HexCoord { get; set; }
        public Point CubeCoord { get; set; }

        public HexEventArgs(Point cubeCoord)
        {
            CubeCoord = cubeCoord;
        }

        public HexEventArgs(HexCoord hexCoord)
        {
            HexCoord = hexCoord;
        }

        //used for changing terrain type of hex
        public HexEventArgs(HexCoord hexCoord, TerrainTypes hexType)
        {
            HexCoord = hexCoord;
            HexType = hexType;
        }

        //used for creation hex
        public HexEventArgs(Point cubeCoord, HexCoord hexCoord, TerrainTypes hexType)
        {
            CubeCoord = cubeCoord;
            HexCoord = hexCoord;
            HexType = hexType;
        }
    }

    public class TextEventArgs : EventArgs
    {
        public string Text { get; set; }

        public TextEventArgs(string text)
        {
            Text = text;
        }
    }

}
