using Assets.HexGridAlgorithms;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MVC.Views
{
    class MapsGenViewPresenter : BaseViewPresenter
    {     
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

        private float _initScale;

        private void Start ()
        {
            CreateHexMapButton.interactable = false;
            CentreMapButton.interactable = false;
            SaveButton.interactable = false;
            NextButton.interactable = false;

            FullScreenButton.onClick.AddListener(ChangeGameWindow);
            ScaleMapSlider.onValueChanged.AddListener(ScaleCameraSize);

            _initScale = MapsCamera.orthographicSize;

            CurrentCanvas = InternalScreenCanvas.transform;

            TerrainTypeButton.transform.GetChild(0).GetComponent<Text>().text = TerrainTypes.Plain.ToString();

            //HexMap = new GameObject("Hex Map");
            //HexMap.transform.parent = CurrentCanvas;
            HexMap.AddComponent<ActionsOnlyInCanvas>();
            HexMap.AddComponent<CameraMove>().PlayerCamera = MapsCamera;
            HexMap.AddComponent<MouseActionsCatcher>().PlayerCamera = MapsCamera;
        }

        public void ChangeGameWindow()
        {
            if (CurrentCanvas == FullScreenCanvas.transform)
            {
                CurrentCanvas = InternalScreenCanvas.transform;
                Panel.SetActive(true);
            }
            else
            {
                CurrentCanvas = FullScreenCanvas.transform;
                Panel.SetActive(false);
            }

            HexMap.transform.parent = CurrentCanvas;
            HexMap.GetComponent<ActionsOnlyInCanvas>().InitComponents();
        }

        public void ScaleCameraSize(float scale)
        {
            MapsCamera.orthographicSize = _initScale / scale;
            CentreMapButton.onClick.Invoke();
        }

        public void OnTerrainTypeWasSet(TerrainTypes terrainType)
        {
            TerrainTypeButton.transform.GetChild(0).GetComponent<Text>().text = terrainType.ToString();
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

        public override void OnMapLoaded()
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
    }
}
