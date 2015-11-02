using System;
using Assets.HexGridAlgorithms;

namespace Assets.MVC.HexAlgorithmsEventArgs
{
    public class PointEventArgs : EventArgs
    {
        public Point CubeCoord { get; set; }

        public PointEventArgs(Point cubeCoord)
        {
            CubeCoord = cubeCoord;
        }
    }
}
