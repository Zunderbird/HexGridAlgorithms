
using System.Collections.Generic;
using Assets.HexGridAlgorithms;

namespace Assets.MVC.Models
{
    class Model
    {
        private int _mapWidthInHex;
        private int _mapHeightInHex;

        private TerrainTypes _currentTerrainType = 0;

        private Vector3D _currentHex;
        private Vector3D _targetHex;

        private Dictionary<Vector3D, Hex> _hexMap;

        public void CreateMap(int mapWidthInHex, int mapHeightInHex)
        {
            CreateMap(mapWidthInHex, mapHeightInHex, _currentTerrainType);
        }

        public void CreateMap(int mapWidthInHex, int mapHeightInHex, TerrainTypes terrainType)
        {
            _hexMap = new Dictionary<Vector3D, Hex>();
        }
    }
}
