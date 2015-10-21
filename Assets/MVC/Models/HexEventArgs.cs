
using System;
using Assets.HexGridAlgorithms;

namespace Assets.MVC.Models
{
    public class HexEventArgs: EventArgs
    {
        public Vector3D HexCoord { get; set; }
        public TerrainTypes HexType { get; set; }

        public HexEventArgs(Vector3D hexCoord)
        {
            HexCoord = hexCoord;
        }

        public HexEventArgs(Vector3D hexCoord, TerrainTypes hexType)
        {
            HexCoord = hexCoord;
            HexType = hexType;
        }
    }

}
