
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MapGame
{
    public class HexLighting : MonoBehaviour
    {
        public enum Distinguish
        {
            TargetHex,
            CurrentHex
        }
        public Distinguish DistinguishMode;

        public Camera PlayerCamera;

        public Color HexColor = Color.grey;
        private readonly Color _defaultHexColor = Color.white;
        
        public Transform Hex { get; private set; }

        private delegate void OperationDelegate(RaycastHit hit);
        private Dictionary<Distinguish, OperationDelegate> _operation;

        private void Start()
        {
            _operation = new Dictionary<Distinguish, OperationDelegate>
            {
                { Distinguish.TargetHex, DistinguishTargetHex },
                { Distinguish.CurrentHex, DistinguishCurrentHex }
            };
        }

        private void Update()
        {
            if (GetComponent<ActionsOnlyInCanvas>() == null ||
                GetComponent<ActionsOnlyInCanvas>().IsMousePosInsideCanvas)
            {
                var ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                    _operation[DistinguishMode](hit);
            }
        }

        private void DistinguishTargetHex(RaycastHit hit)
        {
            if (Hex && Hex != hit.transform)
            {
                Hex.GetComponent<Renderer>().material.color = _defaultHexColor;
            }
            Hex = hit.transform;
            Hex.GetComponent<Renderer>().material.color = HexColor;

        }

        private void DistinguishCurrentHex(RaycastHit hit)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Hex)
                {
                    Hex.GetComponent<Renderer>().material.color = _defaultHexColor;
                }
                Hex = hit.transform;
                Hex.GetComponent<Renderer>().material.color = HexColor;
                Debug.Log(Hex);
            }
        }
    }
}
