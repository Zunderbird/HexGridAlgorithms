using UnityEngine;

namespace Assets.HexGridAlgorithms
{
    public static class MapsGenerator
    {
        public static TerrainTypes MainTerrainType { get; set; }

        public static GameObject GenerateMap(int mapWidthInHex, int mapHeightInHex)
        {
            var map = (new GameObject("Map")).transform;

            GenerateMap(mapWidthInHex, mapHeightInHex, map);

            return map.gameObject;
        }

        public static void GenerateMap(int mapWidthInHex, int mapHeightInHex, Transform map)
        {
            var extent = HexGenerator.HexSize;

            for (var x = 0; x < mapWidthInHex; x++)
            {
                for (var y = 0; y < mapHeightInHex; y++)
                {
                    var hex = HexGenerator.MakeHex().transform;

                    hex.GetComponent<Renderer>().material.mainTexture = TerrainTextures.GetTexture(MainTerrainType);
                    HexGenerator.SetHexInfo(x, y, hex, MainTerrainType);

                    var posY = y * (extent.y * 1.5f);
                    var posX = x * (extent.x * 2f);

                    posX += (y%2 == 0) ? extent.x : 0;

                    hex.position = new Vector3(posX, posY);
                    hex.parent = map;
                }
            }
        }
    }
}

