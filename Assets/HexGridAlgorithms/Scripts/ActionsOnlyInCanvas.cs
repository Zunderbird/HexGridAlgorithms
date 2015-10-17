using UnityEngine;

namespace Assets.HexGridAlgorithms.Scripts
{
    public class ActionsOnlyInCanvas : MonoBehaviour
    {
        public bool IsMousePosInsideCanvas { get; private set; }

        private float _mLeftBorder;
        private float _mRightBorder;
        private float _mUpBorder;
        private float _mBottomBorder;

        void Start()
        {
            _mLeftBorder = transform.position.x - GetComponent<RectTransform>().rect.width / 2;
            _mRightBorder = transform.position.x + GetComponent<RectTransform>().rect.width / 2;
            _mBottomBorder = transform.position.y - GetComponent<RectTransform>().rect.height / 2;
            _mUpBorder = transform.position.y + GetComponent<RectTransform>().rect.height / 2;
        }

        void Update()
        {
            IsMousePosInsideCanvas = IsInsideCanvas(Input.mousePosition);
        }

        private bool IsInsideCanvas(Vector3 position)
        {
            return (_mLeftBorder <= position.x && position.x <= _mRightBorder &&
                    _mBottomBorder <= position.y && position.y <= _mUpBorder);
        }
    }
}

