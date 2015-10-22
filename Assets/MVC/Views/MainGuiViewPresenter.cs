using Assets.HexGridAlgorithms;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MVC.Views
{
    class MainGuiViewPresenter : MonoBehaviour
    {
        public Canvas FullScreenCanvas;
        public Canvas MedievalScreenCanvas;    
        public GameObject Panel;
        public Button CreateHexMapButton;
        public Button TerrainTypeButton;
        public Button CentreMapButton;
        public Button FullScreenButton;
        public InputField MapWidthInputField;
        public InputField MapHeightInputField;
        public Text DistanceText;
        public Slider ScaleMapSlider;
        public GameObject HexMap;
        public Camera MapsCamera;

        private float _initScale;
        private Transform _currentCanvas;

        private readonly Color _targetHexColor = Color.gray / 2;
        private readonly Color _currentHexColor = Color.cyan / 2;

        private Transform _currentHex;
        private Transform _targetHex;

        private void Start ()
        {
            CreateHexMapButton.interactable = false;
            CentreMapButton.interactable = false;

            FullScreenButton.onClick.AddListener(ChangeGameWindow);
            ScaleMapSlider.onValueChanged.AddListener(ScaleCameraSize);

            _initScale = MapsCamera.orthographicSize;

            _currentCanvas = MedievalScreenCanvas.transform;

            TerrainTypeButton.transform.GetChild(0).GetComponent<Text>().text = TerrainTypes.Plain.ToString();
        }

        public void ChangeGameWindow()
        {
            if (_currentCanvas == FullScreenCanvas.transform)
            {
                _currentCanvas = MedievalScreenCanvas.transform;
                Panel.SetActive(true);
            }
            else
            {
                _currentCanvas = FullScreenCanvas.transform;
                Panel.SetActive(false);
            }

            HexMap.transform.parent = _currentCanvas;
            HexMap.GetComponent<ActionsOnlyInCanvas>().InitComponents();
        }

        public void ScaleCameraSize(float scale)
        {
            MapsCamera.orthographicSize = _initScale / scale;
        }

        public void OnHexCreated(Vector3D hexCoord, TerrainTypes hexType)
        {
            var hex = HexGenerator.MakeHex().transform;

            hex.GetComponent<Renderer>().material.mainTexture = TerrainTextures.GetTexture(hexType);
            HexGenerator.SetHexInfo(hexCoord.X, hexCoord.Y, hex);

            hex.position = HexGenerator.CorrelateCoordWithMap(hexCoord);
            hex.parent = HexMap.transform;
        }

        public void OnTerrainTypeWasSet(TerrainTypes terrainType)
        {
            TerrainTypeButton.transform.GetChild(0).GetComponent<Text>().text = terrainType.ToString();
        }

        public void OnMapsCentreCoordsFound(Vector3D hexCoord)
        {
            MapsCamera.transform.position = HexGenerator.CorrelateCoordWithMap(hexCoord, new Vector3(0, 0, MapsCamera.transform.position.z));
        }

        public void OnWidthCorrected(int width)
        {
            MapWidthInputField.GetComponent<InputField>().text = width.ToString();
        }

        public void OnHeightCorrected(int height)
        {
            MapHeightInputField.GetComponent<InputField>().text = height.ToString();
        }

        public void OnDeleteHexMap()
        {
            for (int i = 0; i < HexMap.transform.childCount; i++)
            {
                Destroy(HexMap.transform.GetChild(i).gameObject);
            }
        }

        public void OnMapCreated()
        {
            CentreMapButton.GetComponent<Button>().interactable = true;
        }

        public void OnCreationMapsAdmissible(bool mapCreationAvailable)
        {
            CreateHexMapButton.GetComponent<Button>().interactable = mapCreationAvailable;
        }

        public void OnIlluminateCurrentHex()
        {
            _currentHex = HexMap.GetComponent<MouseActionsCatcher>().CurrentHex;
            _currentHex.GetComponent<Renderer>().material.color += _currentHexColor;
        }

        public void OnIlluminateTargerHex()
        {
            _targetHex = HexMap.GetComponent<MouseActionsCatcher>().CurrentHex;
            _targetHex.GetComponent<Renderer>().material.color += _targetHexColor;
        }

        public void OnSkipTargetHexIllumination()
        {
            _targetHex.GetComponent<Renderer>().material.color -= _targetHexColor;
        }

        public void OnSkipCurrentHexIllumination()
        {
            _currentHex.GetComponent<Renderer>().material.color -= _currentHexColor;
        }

        public void OnPaintHex(TerrainTypes terrainType)
        {
            _currentHex = HexMap.GetComponent<MouseActionsCatcher>().CurrentHex;
            _currentHex.GetComponent<Renderer>().material.mainTexture = TerrainTextures.GetTexture(terrainType);
        }

        public void OnUpdateDistance(string text)
        {
            DistanceText.text = text;
        }
    }
}
