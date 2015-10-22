namespace Assets.HexGridAlgorithms
{
    public class Hex
    {
        public TerrainTypes Type { get; set; }

        public Hex(TerrainTypes terrainType)
        {
            Type = terrainType;
        }
    }
}

