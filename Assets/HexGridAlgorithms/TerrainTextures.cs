using System.Collections.Generic;
using UnityEngine;

namespace Assets.HexGridAlgorithms
{
    static class TerrainTextures
    {
        public static bool IsTexturesLoaded { get; private set; }

        private static readonly Dictionary<TerrainTypes, string> _textureNames = new Dictionary<TerrainTypes, string>
        {
            {TerrainTypes.Plain, "01_Plain_Texture"},
            {TerrainTypes.Forest, "02_Forest_Texture"},
            {TerrainTypes.Hill, "03_Hill_Texture"},
            {TerrainTypes.Lake, "04_Lake_Texture"},
            {TerrainTypes.Swamp, "05_Swamp_Texture"}
        };

        private static readonly Dictionary<TerrainTypes, Texture2D> _textures = new Dictionary<TerrainTypes, Texture2D>();

        public static void LoadTextures()
        {
            foreach (var textureName in _textureNames)
            {
                var texture = (Texture2D)Resources.Load(textureName.Value);
                _textures.Add(textureName.Key, texture);
            }
            IsTexturesLoaded = true;
        }

        public static Texture2D GetTexture(TerrainTypes terrainType)
        {
            return _textures[terrainType];
        }

    }
}
