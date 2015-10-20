using UnityEngine;

namespace Assets.Scripts.MapGame
{
    public class HexSelectionByMouse : MonoBehaviour
    {
        public Camera PlayerCamera;

        public Color SelectedHexColor = Color.blue;
        public Color NextHexColor = Color.green;
        public Color DefaultHexColor = Color.white;

        public Transform CurrentHex { get; private set; }
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
                    if (TargetHex != CurrentHex)
                    {
                        if (TargetHex != hit.transform)
                        {
                            TargetHex.GetComponent<Renderer>().material.color = DefaultHexColor;
                        }
                        TargetHex = hit.transform;
                        TargetHex.GetComponent<Renderer>().material.color = (TargetHex != CurrentHex)
                            ? NextHexColor
                            : SelectedHexColor;
                    }

                    if (hit.transform != CurrentHex)
                    {
                        TargetHex = hit.transform;
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (CurrentHex)
                        {
                            CurrentHex.GetComponent<Renderer>().material.color = DefaultHexColor;
                        }
                        CurrentHex = hit.transform;
                        CurrentHex.GetComponent<Renderer>().material.color = SelectedHexColor;
                    }
                }
            }
        }
    }
}
