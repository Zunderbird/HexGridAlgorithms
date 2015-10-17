using System.Collections.Generic;

namespace Assets.HexGridAlgorithms
{
    class MapController
    {
        private Dictionary<Vector3D, Hex> _mMapData;

        MapController()
        {
            _mMapData = new Dictionary<Vector3D, Hex>();
        }
    }
}


