using UnityEngine;
using UnityEngine.UI;

namespace Assets.HexGridAlgorithms.Scripts
{
    public class ScaleMapSlider : MonoBehaviour
    {
        private Transform _mapTransform;
        private Vector3 _initScale;

        void Start()
        {
            var input = gameObject.GetComponent<Slider>();
            input.onValueChanged.AddListener(ScaleMap);
        }

        public void ScaleMap(float scale)
        {
            if (_mapTransform == null)
            {
                _mapTransform = GameObject.Find("Map").GetComponent<Transform>().transform;
                _initScale = _mapTransform.localScale;
            }
            _mapTransform.localScale = (_initScale * scale);
        }
    }

}

