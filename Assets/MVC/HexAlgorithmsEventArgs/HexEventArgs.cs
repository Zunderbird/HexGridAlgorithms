using Assets.HexGridAlgorithms;

namespace Assets.MVC.HexAlgorithmsEventArgs
{
    public class HexEventArgs : TerrainEventArgs
    {
        public Point CubeCoord { get; set; }

        public HexEventArgs(Point cubeCoord, HexCoord hexCoord, TerrainTypes hexType) : base(hexCoord, hexType)
        {
            CubeCoord = cubeCoord;
        }
    }
}
