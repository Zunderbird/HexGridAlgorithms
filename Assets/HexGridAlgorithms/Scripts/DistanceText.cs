using UnityEngine;

namespace Assets.HexGridAlgorithms.Scripts
{
    public class DistanceText : MonoBehaviour
    {
        public GameObject MapCanvas;

        void Update()
        {
            if (MapCanvas != null && MapCanvas.GetComponent<HexSelection>() != null)
            {
                var currentHex = MapCanvas.GetComponent<HexSelection>().CurrentHex;
                var targetHex = MapCanvas.GetComponent<HexSelection>().TargetHex;

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
}

