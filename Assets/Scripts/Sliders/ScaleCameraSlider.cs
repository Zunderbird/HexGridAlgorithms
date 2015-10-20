// Attach this script to UnityEngine.UI.Slider
// Script provides oppotunity to scale camera with help of slider and should receive a Camera as parametr.

using UnityEngine;

namespace Assets.Scripts.Sliders
{
    public class ScaleCameraSlider : MonoBehaviour
    {
        private float _initScale;

        public Camera MapsCamera;

        private void Start()
        {
            _initScale = MapsCamera.orthographicSize;

            var input = gameObject.GetComponent<UnityEngine.UI.Slider>();
            input.onValueChanged.AddListener(ScaleMap);
        }

        private void ScaleMap(float scale)
        {
            MapsCamera.orthographicSize = _initScale/scale;
        }
    }

}

