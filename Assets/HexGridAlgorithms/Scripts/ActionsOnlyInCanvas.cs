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
            InitComponents();
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

        public void InitComponents()
        {
            var position = transform.parent.position;
            var rect = transform.parent.GetComponent<RectTransform>().rect;

            _mLeftBorder = position.x - rect.width / 2;
            _mRightBorder = position.x + rect.width / 2;
            _mBottomBorder = position.y - rect.height / 2;
            _mUpBorder = position.y + rect.height / 2;
        }
    }
}

