using Assets.HexGridAlgorithms;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MVC.Views
{
    class MapsGenViewPresenter : MonoBehaviour
    {
        public Canvas FullScreenCanvas;
        public Canvas MedievalScreenCanvas;    
        public GameObject Panel;
        public Button CreateHexMapButton;
        public Button TerrainTypeButton;
        public Button CentreMapButton;
        public Button FullScreenButton;
        public Button SaveButton;
        public Button NextButton;
        public InputField MapWidthInputField;
        public InputField MapHeightInputField;
        public Text DistanceText;
        public Slider ScaleMapSlider;        
        public Camera MapsCamera;

        public GameObject HexMap { get; private set; }

        private float _initScale;
        private Transform _currentCanvas;

        private readonly Color _targetHexColor = Color.gray / 2;
        private readonly Color _currentHexColor = Color.cyan / 2;

        private void Start ()
        {
            CreateHexMapButton.interactable = false;
            CentreMapButton.interactable = false;
            SaveButton.interactable = false;
            NextButton.interactable = false;

            FullScreenButton.onClick.AddListener(ChangeGameWindow);
            ScaleMapSlider.onValueChanged.AddListener(ScaleCameraSize);

            _initScale = MapsCamera.orthographicSize;

            _currentCanvas = MedievalScreenCanvas.transform;

            TerrainTypeButton.transform.GetChild(0).GetComponent<Text>().text = TerrainTypes.Plain.ToString();

            HexMap = new GameObject("Hex Map");
            HexMap.transform.parent = _currentCanvas;
            HexMap.AddComponent<ActionsOnlyInCanvas>();
            HexMap.AddComponent<CameraMove>().PlayerCamera = MapsCamera;
            HexMap.AddComponent<MouseActionsCatcher>().PlayerCamera = MapsCamera;
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
            CentreMapButton.onClick.Invoke();
        }

        public void OnHexCreated(Point cubeCoord, HexCoord hexCoord, TerrainTypes hexType)
        {
            var hex = HexGenerator.MakeHex().transform;

            hex.GetComponent<Renderer>().material.mainTexture = TerrainTextures.GetTexture(hexType);
            HexGenerator.SetHexInfo(hexCoord, hex);

            hex.position = HexGenerator.CorrelateCoordWithMap(cubeCoord);
            hex.parent = HexMap.transform;
        }

        public void OnTerrainTypeWasSet(TerrainTypes terrainType)
        {
            TerrainTypeButton.transform.GetChild(0).GetComponent<Text>().text = terrainType.ToString();
        }

        public void OnMapsCentreCoordsFound(Point cubeCoord)
        {
            CentreTheMap(cubeCoord);
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
            for (var i = 0; i < HexMap.transform.childCount; i++)
            {
                Destroy(HexMap.transform.GetChild(i).gameObject);
            }
        }

        public void OnMapCreated()
        {
            CentreMapButton.GetComponent<Button>().interactable = true;
            CentreMapButton.onClick.Invoke();
            SaveButton.interactable = true;
            NextButton.interactable = true;
        }

        public void OnCreationMapsAdmissible(bool mapCreationAvailable)
        {
            CreateHexMapButton.GetComponent<Button>().interactable = mapCreationAvailable;
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

        public void OnPaintHex(HexCoord hexCoord, TerrainTypes terrainType)
        {
            HexMap.transform.Find(hexCoord.ToString()).GetComponent<Renderer>().material.mainTexture = TerrainTextures.GetTexture(terrainType);
        }

        public void OnUpdateDistance(string text)
        {
            DistanceText.text = text;
        }

        public void OnLoadingNextStage()
        {
            Application.LoadLevel("Scene_02");
        }

        private void CentreTheMap(Point cubeCoord)
        {
            MapsCamera.transform.position = HexGenerator.CorrelateCoordWithMap(cubeCoord, new Vector3(0, 0, MapsCamera.transform.position.z));
            MapsCamera.transform.position += CorrelateWithCanvas();
        }

        private Vector3 CorrelateWithCanvas()
        {
            var correction = (FullScreenCanvas.transform.position - _currentCanvas.transform.position) * MapsCamera.nearClipPlane * MapsCamera.orthographicSize/100;
            return correction;
        }
    }
}
