namespace Assets.HexGridAlgorithms
{
    public struct Hex
    {
        public TerrainTypes Type { get; set; }

        public Hex(TerrainTypes terrainType)
        {
            Type = terrainType;
        }
    }
}

