
using Assets.HexGridAlgorithms;
using Assets.Scripts.MapGame;
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

        private void Start ()
        {
            CreateHexMapButton.interactable = false;
            CentreMapButton.interactable = false;
            //CreateHexMapButton.GetComponent<Button>().interactable = false;
            //CentreMapButton.GetComponent<Button>().interactable = false;

            FullScreenButton.onClick.AddListener(ChangeGameWindow);
            ScaleMapSlider.onValueChanged.AddListener(ScaleCameraSize);
            //FullScreenButton.GetComponent<Button>().onClick.AddListener(ChangeGameWindow);
            //ScaleMapSlider.GetComponent<Slider>().onValueChanged.AddListener(ScaleCameraSize);

            _initScale = MapsCamera.orthographicSize;

            _currentCanvas = MedievalScreenCanvas.transform;
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

            var posY = hexCoord.Y * (HexGenerator.HexSize.y * 1.5f);
            var posX = hexCoord.X * (HexGenerator.HexSize.x * 2f);

            posX += (hexCoord.Y % 2 == 0) ? HexGenerator.HexSize.x : 0;

            hex.position = new Vector3(posX, posY);
            hex.parent = HexMap.transform;
        }

        public void OnTerrainTypeWasSet(TerrainTypes terrainType)
        {
            TerrainTypeButton.transform.GetChild(0).GetComponent<Text>().text = terrainType.ToString();
        }

        public void OnMapsCentreCoordsFound(Vector3D hexCoord)
        {
            var posY = hexCoord.Y * (HexGenerator.HexSize.y * 1.5f);
            var posX = hexCoord.X * (HexGenerator.HexSize.x * 2f);

            posX += (hexCoord.Y % 2 == 0) ? HexGenerator.HexSize.x : 0;
            Debug.Log(HexMap.transform.parent.position);
            MapsCamera.transform.position = new Vector3(posX, posY, MapsCamera.transform.position.z);

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
    }
}
