using UnityEngine;
using System.Collections;

namespace Assets.HexGridAlgorithms.Scripts
{
    public class CameraMove : MonoBehaviour
    {
        public Camera PlayerCamera;

        private Vector3 _mMouseButtonStartPos;

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
                    PlayerCamera.transform.position += (_mMouseButtonStartPos - Input.mousePosition)/100;
                    _mMouseButtonStartPos = Input.mousePosition;
                }
            }
        }
    }
}

