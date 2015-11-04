using Assets.MVC.Models;
using Assets.MVC.Views;
using Assets.Scripts;
using UnityEngine;

namespace Assets.MVC.Controllers
{
    internal class HexAlgoController
    {
        private readonly HexAlgoModel _hexAlgoModel;

        private readonly HexAlgoViewPresenter _hexAlgoGui;

        public HexAlgoController()
        {
            _hexAlgoGui = GameObject.Find("MainGUI").GetComponent<HexAlgoViewPresenter>();

            _hexAlgoModel = new HexAlgoModel();

            //Views
            _hexAlgoGui.HexMap.GetComponent<MouseActionsCatcher>().HexSelected += (sender, args) => _hexAlgoModel.SelectHex(args.HexCoord);
            _hexAlgoGui.HexMap.GetComponent<MouseActionsCatcher>().HexHitted += (sender, args) => _hexAlgoModel.HitHex(args.HexCoord);
            _hexAlgoGui.HexMap.GetComponent<MouseActionsCatcher>().NoHexHitted += (sender, args) => _hexAlgoModel.SkipHittedHex();
            _hexAlgoGui.HexMap.GetComponent<MouseActionsCatcher>().NoHexSelected += (sender, args) => _hexAlgoModel.SkipSelectedHex();

            //Models
            _hexAlgoModel.HexCreated += (sender, args) => _hexAlgoGui.OnHexCreated(args.CubeCoord, args.HexCoord, args.HexType);
            _hexAlgoModel.MapsCentreCoordsFound += (sender, args) => _hexAlgoGui.OnMapsCentreCoordsFound(args.CubeCoord);

            _hexAlgoModel.MapLoaded += (sender, args) => _hexAlgoGui.OnMapLoaded();

            _hexAlgoModel.IlluminateCurrentHex += (sender, args) => _hexAlgoGui.OnIlluminateCurrentHex(args.HexCoord);
            _hexAlgoModel.IlluminateTargetHex += (sender, args) => _hexAlgoGui.OnIlluminateTargerHex(args.HexCoord);
            _hexAlgoModel.SkipTargetHexIllumination += (sender, args) => _hexAlgoGui.OnSkipTargetHexIllumination(args.HexCoord);
            _hexAlgoModel.SkipCurrentHexIllumination += (sender, args) => _hexAlgoGui.OnSkipCurrentHexIllumination(args.HexCoord);

            _hexAlgoModel.LoadMap();
            _hexAlgoModel.FindMapsCentreCoords();
        }
    }
}
