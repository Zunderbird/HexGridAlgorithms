
using UnityEngine;

namespace Assets.Scripts.MapGame
{
    class TargetHexSelection : MonoBehaviour
    {
        public Camera PlayerCamera;

        public Color TargetHexColor = Color.green;
        private readonly Color _defaultHexColor = Color.white;

        public Transform TargetHex { get; private set; }

        void Update()
        {
            if (GetComponent<ActionsOnlyInCanvas>() == null ||
                GetComponent<ActionsOnlyInCanvas>().IsMousePosInsideCanvas)
            {
                var ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (TargetHex && TargetHex != hit.transform)
                    {
                        TargetHex.GetComponent<Renderer>().material.color = _defaultHexColor;
                    }
                    TargetHex = hit.transform;
                    TargetHex.GetComponent<Renderer>().material.color = TargetHexColor;                                 
                }
            }
        }
    }
}
