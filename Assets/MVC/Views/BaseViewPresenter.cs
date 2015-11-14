using System;
using Assets.HexGridAlgorithms;
using Assets.MVC.HexAlgorithmsEventArgs;
using UnityEditor;
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

        public delegate void TextEvent(object sender, TextEventArgs args);
        public event TextEvent SaveMapDialog;
        public event TextEvent LoadMapDialog;

        public virtual void OnMapLoaded()
        {
        }

        public void OnDeleteHexMap()
        {
            for (var i = 0; i < HexMap.transform.childCount; i++)
            {
                Destroy(HexMap.transform.GetChild(i).gameObject);
            }
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

        public void SaveDialog()
        {
            var path = EditorUtility.SaveFilePanel("Save map as JSON", "Assets/HexMaps", "NewMap.json", "json");
            if (SaveMapDialog != null && path != string.Empty) SaveMapDialog(this, new TextEventArgs(path));
        }

        public void LoadDialog()
        {
            var path = EditorUtility.OpenFilePanel("Load map", "Assets/HexMaps", "json");
            if (LoadMapDialog != null && path != string.Empty) LoadMapDialog(this, new TextEventArgs(path));
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
