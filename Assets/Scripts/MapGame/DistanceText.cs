using Assets.HexGridAlgorithms;
using UnityEngine;

namespace Assets.Scripts.MapGame
{
    public class DistanceText : MonoBehaviour
    {
        public GameObject MapCanvas;

        void Update()
        {
            var currentHex = MapCanvas.GetComponent<HexSelectionByMouse>().CurrentHex;
            var targetHex = MapCanvas.GetComponent<HexSelectionByMouse>().TargetHex;

            if (currentHex && targetHex)
            {
                var currentHexInfo = currentHex.GetComponent<HexData>().HexPosition;
                var targetHexInfo = targetHex.GetComponent<HexData>().HexPosition;
                ;

                var moveDistance = HexAlgorithms.CalculateDistance(currentHexInfo.ToVector3D(),
                    targetHexInfo.ToVector3D());

                var distanceText = GetComponent<UnityEngine.UI.Text>();
                distanceText.text = "Current Hex : \n" + currentHexInfo + "   Target Hex : \n" + targetHexInfo +
                                    "   Distance : " + moveDistance;
            }
        }
    }
}

