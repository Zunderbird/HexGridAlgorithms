using UnityEngine;

namespace Assets.HexGridAlgorithms.Scripts
{
    public class CameraMove : MonoBehaviour
    {
        public Camera PlayerCamera;

        private Vector3 _mMouseButtonStartPos;
        private float _initScale;

        void Start()
        {
            _initScale = PlayerCamera.orthographicSize;
        }

        void Update()
        {
            if (GetComponent<ActionsOnlyInCanvas>() == null ||
                GetComponent<ActionsOnlyInCanvas>().IsMousePosInsideCanvas)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    _mMouseButtonStartPos = Input.mousePosition;
                }

                if (Input.GetMouseButton(1))
                {
                    var correlation = _initScale/PlayerCamera.orthographicSize;
                    PlayerCamera.transform.position += (_mMouseButtonStartPos - Input.mousePosition)/correlation/100;
                    _mMouseButtonStartPos = Input.mousePosition;
                }
            }
        }
    }
}

