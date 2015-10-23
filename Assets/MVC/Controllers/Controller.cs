using Assets.MVC.Models;
using Assets.MVC.Views;
using Assets.Scripts;
using UnityEngine;

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

            //Views
            _mainGui.CreateHexMapButton.onClick.AddListener(_model.CreateHexMap);
            _mainGui.TerrainTypeButton.onClick.AddListener(_model.SetNextTerrainType);
            _mainGui.CentreMapButton.onClick.AddListener(_model.FindMapsCentreCoords);

            _mainGui.MapWidthInputField.onValueChange.AddListener(_model.SetWidthInHex);
            _mainGui.MapHeightInputField.onValueChange.AddListener(_model.SetHeightInHex);

            _mainGui.HexMap.GetComponent<MouseActionsCatcher>().HexSelected += (args) => _model.SelectHex(args.HexCoord);
            _mainGui.HexMap.GetComponent<MouseActionsCatcher>().HexHitted += (args) => _model.HitHex(args.HexCoord);
            _mainGui.HexMap.GetComponent<MouseActionsCatcher>().NoHexHitted += (sender, args) => _model.NoHittedHex();

            //Models
            _model.HexCreated += (args) => _mainGui.OnHexCreated(args.CubeCoord, args.HexCoord, args.HexType);
            _model.MapsCentreCoordsFound += (args) => _mainGui.OnMapsCentreCoordsFound(args.CubeCoord);

            _model.TerrainTypeWasSet += (sender, args) => _mainGui.OnTerrainTypeWasSet(_model.CurrentTerrainType);
            _model.WidthCorrected += (sender, args) => _mainGui.OnWidthCorrected(_model.MapWidthInHex);
            _model.HeightCorrected += (sender, args) => _mainGui.OnHeightCorrected(_model.MapHeightInHex);

            _model.DeleteHexMap += (sender, args) => _mainGui.OnDeleteHexMap();
            _model.MapCreated += (sender, args) => _mainGui.OnMapCreated();
            _model.CreationMapsAdmissible += (sender, args) => _mainGui.OnCreationMapsAdmissible(true);
            _model.CreationMapsInadmissible += (sender, args) => _mainGui.OnCreationMapsAdmissible(false);

            _model.IlluminateCurrentHex += (args) => _mainGui.OnIlluminateCurrentHex(args.HexCoord);
            _model.IlluminateTargetHex += (args) => _mainGui.OnIlluminateTargerHex(args.HexCoord);
            _model.SkipTargetHexIllumination += (args) => _mainGui.OnSkipTargetHexIllumination(args.HexCoord);
            _model.SkipCurrentHexIllumination += (args) => _mainGui.OnSkipCurrentHexIllumination(args.HexCoord);

            _model.PaintHex += args => _mainGui.OnPaintHex(args.HexCoord, args.HexType);
            _model.UpdateDistance += (args) => _mainGui.OnUpdateDistance(args.Text);
        }
    }
}
