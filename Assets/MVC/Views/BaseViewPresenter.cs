using System;
using Assets.HexGridAlgorithms;
using UnityEngine;

namespace Assets.MVC.Views
{
    class BaseViewPresenter : MonoBehaviour
    {
        public Camera MapsCamera;
        public GameObject HexMap;

        public Canvas FullScreenCanvas;
        public Canvas InternalScreenCanvas;

        protected Transform CurrentCanvas;

        private readonly Color _targetHexColor = Color.gray / 2;
        private readonly Color _currentHexColor = Color.cyan / 2;

        public event EventHandler Initialized;

        public virtual void OnMapLoaded()
        {
        }

        protected void OnInitialized()
        {
            var handler = Initialized;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void OnHexCreated(Point cubeCoord, HexCoord hexCoord, TerrainTypes hexType)
        {
            var hex = HexGenerator.MakeHex().transform;

            hex.GetComponent<Renderer>().material.mainTexture = TerrainTextures.GetTexture(hexType);
            HexGenerator.SetHexInfo(hexCoord, hex);

            hex.position = HexGenerator.CorrelateCoordWithMap(cubeCoord);
            hex.parent = HexMap.transform;
        }

        public void OnIlluminateCurrentHex(HexCoord hexCoord)
        {
            HexMap.transform.Find(hexCoord.ToString()).GetComponent<Renderer>().material.color += _currentHexColor;
        }

        public void OnIlluminateTargerHex(HexCoord hexCoord)
        {
            HexMap.transform.Find(hexCoord.ToString()).GetComponent<Renderer>().material.color += _targetHexColor;
        }

        public void OnSkipTargetHexIllumination(HexCoord hexCoord)
        {
            HexMap.transform.Find(hexCoord.ToString()).GetComponent<Renderer>().material.color -= _targetHexColor;
        }

        public void OnSkipCurrentHexIllumination(HexCoord hexCoord)
        {
            HexMap.transform.Find(hexCoord.ToString()).GetComponent<Renderer>().material.color -= _currentHexColor;
        }

        public void OnMapsCentreCoordsFound(Point cubeCoord)
        {
            CentreTheMap(cubeCoord);
        }

        protected void CentreTheMap(Point cubeCoord)
        {
            MapsCamera.transform.position = HexGenerator.CorrelateCoordWithMap(cubeCoord, new Vector3(0, 0, MapsCamera.transform.position.z));
            MapsCamera.transform.position += CorrelateWithCanvas();
        }

        private Vector3 CorrelateWithCanvas()
        {
            var correction = (FullScreenCanvas.transform.position - CurrentCanvas.transform.position) * MapsCamera.nearClipPlane * MapsCamera.orthographicSize / 100;
            return correction;
        }
    }
}
