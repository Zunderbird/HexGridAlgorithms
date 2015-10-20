// Attach this script to UI.Slider
// Script provides oppotunity to scale camera with help of slider and should receive a Camera as parametr.

using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ScaleCameraSlider : MonoBehaviour
    {
        private float _initScale;

        public Camera MapsCamera;

        private void Start()
        {
            _initScale = MapsCamera.orthographicSize;

            var input = gameObject.GetComponent<Slider>();
            input.onValueChanged.AddListener(ScaleMap);
        }

        private void ScaleMap(float scale)
        {
            MapsCamera.orthographicSize = _initScale/scale;
        }
    }

}

