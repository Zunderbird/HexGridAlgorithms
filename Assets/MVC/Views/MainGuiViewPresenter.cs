
using UnityEngine.UI;

namespace Assets.MVC.Views
{
    class MainGuiViewPresenter : ViewPresenter
    {
        public ButtonViewPresenter CreateHexMapButton;
        public ButtonViewPresenter TerrainTypeButton;
        public ButtonViewPresenter CentreMapButton;
        public ButtonViewPresenter FullScreenButton;

        public InputFieldViewPresenter MapWidthInputField;
        public InputFieldViewPresenter MapHeightInputField;

        public SliderViewPresenter ScaleMapSlider;

        MainGuiViewPresenter()
        {
            CreateHexMapButton.GetComponent<Button>().onClick.AddListener(CreateHexMap);
            TerrainTypeButton.GetComponent<Button>().onClick.AddListener(ChangeTerrainType);
            CentreMapButton.GetComponent<Button>().onClick.AddListener(CentreMap);
            FullScreenButton.GetComponent<Button>().onClick.AddListener(ChangeGameWindow);

            MapWidthInputField.GetComponent<InputField>().onValueChange.AddListener(SetWidthInHex);
            MapHeightInputField.GetComponent<InputField>().onValueChange.AddListener(SetHeightInHex);

            ScaleMapSlider.GetComponent<Slider>().onValueChanged.AddListener(ScaleCameraSize);
        }

        void CreateHexMap()
        { }

        void ChangeTerrainType()
        { }

        void CentreMap()
        { }

        void ChangeGameWindow()
        { }

        void SetWidthInHex(string arg)
        { }

        void SetHeightInHex(string arg)
        { }

        void ScaleCameraSize(float scale)
        { }
    }
}
