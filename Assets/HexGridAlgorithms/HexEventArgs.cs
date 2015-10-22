using System;

namespace Assets.HexGridAlgorithms
{
    public class HexEventArgs: EventArgs
    {
        public Vector3D HexCoord { get; set; }
        public TerrainTypes HexType { get; set; }

        public HexEventArgs(Vector3D hexCoord)
        {
            HexCoord = hexCoord;
        }

        public HexEventArgs(TerrainTypes hexType)
        {
            HexType = hexType;
        }

        public HexEventArgs(Vector3D hexCoord, TerrainTypes hexType)
        {
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
