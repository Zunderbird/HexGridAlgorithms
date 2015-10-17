using UnityEngine;
using UnityEngine.UI;

namespace Assets.HexGridAlgorithms.Scripts
{
    public class ScaleMapSlider : MonoBehaviour
    {
        private float _initScale;

        public Camera MapsCamera;

        void Start()
        {
            _initScale = MapsCamera.orthographicSize;

            var input = gameObject.GetComponent<Slider>();
            if (input != null)
                input.onValueChanged.AddListener(ScaleMap);
        }

        public void ScaleMap(float scale)
        {
            MapsCamera.orthographicSize = _initScale/scale;
        }
    }

}

