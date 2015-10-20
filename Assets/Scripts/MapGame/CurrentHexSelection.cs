
using UnityEngine;

namespace Assets.Scripts.MapGame
{
    class CurrentHexSelection : MonoBehaviour
    {
        public Camera PlayerCamera;

        public Color SelectedHexColor = Color.gray;
        private readonly Color _defaultHexColor = Color.white;

        public Transform CurrentHex { get; private set; }

        void Update()
        {
            if (GetComponent<ActionsOnlyInCanvas>() == null ||
                GetComponent<ActionsOnlyInCanvas>().IsMousePosInsideCanvas)
            {
                var ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (CurrentHex)
                        {
                            CurrentHex.GetComponent<Renderer>().material.color = _defaultHexColor;
                        }
                        CurrentHex = hit.transform;
                        CurrentHex.GetComponent<Renderer>().material.color = SelectedHexColor;
                    }
                }
            }
        }
    }
}
