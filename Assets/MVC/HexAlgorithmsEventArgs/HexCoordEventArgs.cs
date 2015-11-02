using System;
using Assets.HexGridAlgorithms;

namespace Assets.MVC.HexAlgorithmsEventArgs
{
    public class HexCoordEventArgs : EventArgs
    {
        public HexCoord HexCoord { get; set; }

        public HexCoordEventArgs(HexCoord hexCoord)
        {
            HexCoord = hexCoord;
        }
    }
}
