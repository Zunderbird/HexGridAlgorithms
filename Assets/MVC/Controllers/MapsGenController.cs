using Assets.MVC.Models;
using Assets.MVC.Views;
using Assets.Scripts;
using UnityEngine;

namespace Assets.MVC.Controllers
{
    class MapsGenController
    {
        private readonly MapsGenModel _mapsGenModel;

        private readonly MapsGenViewPresenter _mapsGenGui;

        public MapsGenController()
        {
            _mapsGenGui = GameObject.Find("MainGUI").GetComponent<MapsGenViewPresenter>();

            _mapsGenModel = new MapsGenModel();

            //Views
            _mapsGenGui.CreateHexMapButton.onClick.AddListener(_mapsGenModel.CreateHexMap);
            _mapsGenGui.TerrainTypeButton.onClick.AddListener(_mapsGenModel.SetNextTerrainType);
            _mapsGenGui.CentreMapButton.onClick.AddListener(_mapsGenModel.FindMapsCentreCoords);
            _mapsGenGui.SaveButton.onClick.AddListener(_mapsGenModel.SaveMap);
            _mapsGenGui.NextButton.onClick.AddListener(_mapsGenModel.NextStage);

            _mapsGenGui.MapWidthInputField.onValueChange.AddListener(_mapsGenModel.SetWidthInHex);
            _mapsGenGui.MapHeightInputField.onValueChange.AddListener(_mapsGenModel.SetHeightInHex);

            _mapsGenGui.HexMap.GetComponent<MouseActionsCatcher>().HexSelected += (sender, args) => _mapsGenModel.SelectHex(args.HexCoord);
            _mapsGenGui.HexMap.GetComponent<MouseActionsCatcher>().HexHitted += (sender, args) => _mapsGenModel.HitHex(args.HexCoord);
            _mapsGenGui.HexMap.GetComponent<MouseActionsCatcher>().NoHexHitted += (sender, args) => _mapsGenModel.SkipHittedHex();
            _mapsGenGui.HexMap.GetComponent<MouseActionsCatcher>().NoHexSelected += (sender, args) => _mapsGenModel.SkipSelectedHex();

            //Models
            _mapsGenModel.HexCreated += (sender, args) => _mapsGenGui.OnHexCreated(args.CubeCoord, args.HexCoord, args.HexType);
            _mapsGenModel.MapsCentreCoordsFound += (sender, args) => _mapsGenGui.OnMapsCentreCoordsFound(args.CubeCoord);

            _mapsGenModel.TerrainTypeWasSet += (sender, args) => _mapsGenGui.OnTerrainTypeWasSet(_mapsGenModel.CurrentTerrainType);
            _mapsGenModel.WidthCorrected += (sender, args) => _mapsGenGui.OnWidthCorrected(_mapsGenModel.MapWidthInHex);
            _mapsGenModel.HeightCorrected += (sender, args) => _mapsGenGui.OnHeightCorrected(_mapsGenModel.MapHeightInHex);

            _mapsGenModel.DeleteHexMap += (sender, args) => _mapsGenGui.OnDeleteHexMap();
            _mapsGenModel.MapCreated += (sender, args) => _mapsGenGui.OnMapCreated();
            _mapsGenModel.CreationMapsAdmissible += (sender, args) => _mapsGenGui.OnCreationMapsAdmissible(true);
            _mapsGenModel.CreationMapsInadmissible += (sender, args) => _mapsGenGui.OnCreationMapsAdmissible(false);

            _mapsGenModel.IlluminateCurrentHex += (sender, args) => _mapsGenGui.OnIlluminateCurrentHex(args.HexCoord);
            _mapsGenModel.IlluminateTargetHex += (sender, args) => _mapsGenGui.OnIlluminateTargerHex(args.HexCoord);
            _mapsGenModel.SkipTargetHexIllumination += (sender, args) => _mapsGenGui.OnSkipTargetHexIllumination(args.HexCoord);
            _mapsGenModel.SkipCurrentHexIllumination += (sender, args) => _mapsGenGui.OnSkipCurrentHexIllumination(args.HexCoord);

            _mapsGenModel.PaintHex += (sender, args) => _mapsGenGui.OnPaintHex(args.HexCoord, args.HexType);
            _mapsGenModel.UpdateDistance += (args) => _mapsGenGui.OnUpdateDistance(args.Text);

            _mapsGenModel.LoadingNextStage += (sender, args) => _mapsGenGui.OnLoadingNextStage();
        }
    }
}
