using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Buttons
{
    public class MapCentreButton : MonoBehaviour
    {

        void Start()
        {
            var input = gameObject.GetComponent<Button>();
            input.onClick.AddListener(PlaceCameraToMapCentre);
        }

        void Update()
        {

        }

        void PlaceCameraToMapCentre()
        {

        }
    }

}

