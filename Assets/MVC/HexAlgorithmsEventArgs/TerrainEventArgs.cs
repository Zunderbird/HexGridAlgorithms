using Assets.HexGridAlgorithms;

namespace Assets.MVC.HexAlgorithmsEventArgs
{
    public class TerrainEventArgs : HexCoordEventArgs
    {
        public TerrainTypes HexType { get; set; }

        public TerrainEventArgs(HexCoord hexCoord, TerrainTypes terrainType) : base(hexCoord)
        {
            HexType = terrainType;
        }
    }
}
