using Assets.MVC.Models;
using Assets.MVC.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MVC.Controllers
{
    class Controller
    {
        private readonly Model _model;

        private readonly MainGuiViewPresenter _mainGui;

        public Controller()
        {
            _mainGui = GameObject.Find("MainGUI").GetComponent<MainGuiViewPresenter>();

            _model = new Model();

            _mainGui.CreateHexMapButton.onClick.AddListener(_model.CreateHexMap);
            _mainGui.TerrainTypeButton.onClick.AddListener(_model.SetNextTerrainType);
            _mainGui.CentreMapButton.onClick.AddListener(_model.FindMapsCentreCoords);

            _mainGui.MapWidthInputField.onValueChange.AddListener(_model.SetWidthInHex);
            _mainGui.MapHeightInputField.onValueChange.AddListener(_model.SetHeightInHex);

            //_mainGui.CreateHexMapButton.GetComponent<Button>().onClick.AddListener(_model.CreateHexMap);
            //_mainGui.TerrainTypeButton.GetComponent<Button>().onClick.AddListener(_model.SetNextTerrainType);
            //_mainGui.CentreMapButton.GetComponent<Button>().onClick.AddListener(_model.FindMapsCentreCoords);

            //_mainGui.MapWidthInputField.GetComponent<InputField>().onValueChange.AddListener(_model.SetWidthInHex);
            //_mainGui.MapHeightInputField.GetComponent<InputField>().onValueChange.AddListener(_model.SetHeightInHex);

            _model.HexCreated += (args) => _mainGui.OnHexCreated(args.HexCoord, args.HexType);
            _model.MapsCentreCoordsFound += (args) => _mainGui.OnMapsCentreCoordsFound(args.HexCoord);

            _model.TerrainTypeWasSet += (sender, args) => _mainGui.OnTerrainTypeWasSet(_model.CurrentTerrainType);
            _model.WidthCorrected += (sender, args) => _mainGui.OnWidthCorrected(_model.MapWidthInHex);
            _model.HeightCorrected += (sender, args) => _mainGui.OnHeightCorrected(_model.MapHeightInHex);

            _model.DeleteHexMap += (sender, args) => _mainGui.OnDeleteHexMap();
            _model.MapCreated += (sender, args) => _mainGui.OnMapCreated();
            _model.CreationMapsAdmissible += (sender, args) => _mainGui.OnCreationMapsAdmissible(true);
            _model.CreationMapsInadmissible += (sender, args) => _mainGui.OnCreationMapsAdmissible(false);
        }

        public void ShowView()
        {          
        }
    }
}
