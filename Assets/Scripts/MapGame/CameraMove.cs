// This script can be attached to any GameObject and should receive a Camera as parametr.
// If GameObject contains script ActionsOnlyInCanvas, it will take into account the boundaries of the parent object.

// This script allows the movement of the camera by pressing the right mouse button.

using UnityEngine;

namespace Assets.Scripts.MapGame
{
    public class CameraMove : MonoBehaviour
    {
        public Camera PlayerCamera;

        private Vector3 _mouseButtonStartPos;
        private float _initScale;

        private void Start()
        {
            _initScale = PlayerCamera.orthographicSize;
        }

        private void Update()
        {
            if (GetComponent<ActionsOnlyInCanvas>() == null ||
                GetComponent<ActionsOnlyInCanvas>().IsMousePosInsideCanvas)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    _mouseButtonStartPos = Input.mousePosition;
                }

                if (Input.GetMouseButton(1))
                {
                    var correlation = _initScale/PlayerCamera.orthographicSize;
                    PlayerCamera.transform.position += (_mouseButtonStartPos - Input.mousePosition)/correlation/100;
                    _mouseButtonStartPos = Input.mousePosition;
                }
            }
        }
    }
}

