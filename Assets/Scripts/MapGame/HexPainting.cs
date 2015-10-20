using Assets.HexGridAlgorithms;
using UnityEngine;

namespace Assets.Scripts.MapGame
{
    public class HexPainting : MonoBehaviour
    {
        public Camera PlayerCamera;

        private Transform _targetHex;

        private void Update()
        {
            if (GetComponent<ActionsOnlyInCanvas>() == null ||
                GetComponent<ActionsOnlyInCanvas>().IsMousePosInsideCanvas)
            {
                var ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {                 
                    if (Input.GetMouseButton(0) && _targetHex != hit.transform)
                    {
                        _targetHex = hit.transform;
                        _targetHex.GetComponent<Renderer>().material.mainTexture = TerrainTextures.GetTexture(MapsGenerator.MainTerrainType);
                    }
                }
            }
        }

    }

}

