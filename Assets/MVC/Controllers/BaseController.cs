using Assets.MVC.Models;
using Assets.MVC.Views;
using Assets.Scripts;

namespace Assets.MVC.Controllers
{
    internal class BaseController
    {
        protected readonly BaseModel Model;

        protected readonly BaseViewPresenter MainGui;

        public BaseController(BaseViewPresenter presenter, BaseModel model)
        {
            MainGui = presenter;
            Model = model;
        }

        public void Start()
        {
            //View's events:
            MainGui.HexMap.GetComponent<MouseActionsCatcher>().HexSelected += (sender, args) => Model.SelectHex(args.HexCoord);
            MainGui.HexMap.GetComponent<MouseActionsCatcher>().HexHitted += (sender, args) => Model.HitHex(args.HexCoord);
            MainGui.HexMap.GetComponent<MouseActionsCatcher>().NoHexHitted += (sender, args) => Model.SkipHittedHex();
            MainGui.HexMap.GetComponent<MouseActionsCatcher>().NoHexSelected += (sender, args) => Model.SkipSelectedHex();

            //Model's events:
            Model.HexCreated += (sender, args) => MainGui.OnHexCreated(args.CubeCoord, args.HexCoord, args.HexType);
            Model.MapsCentreCoordsFound += (sender, args) => MainGui.OnMapsCentreCoordsFound(args.CubeCoord);

            Model.MapLoaded += (sender, args) => MainGui.OnMapLoaded();
            Model.DeleteHexMap += (sender, args) => MainGui.OnDeleteHexMap();

            Model.IlluminateCurrentHex += (sender, args) => MainGui.OnIlluminateCurrentHex(args.HexCoord);
            Model.IlluminateTargetHex += (sender, args) => MainGui.OnIlluminateTargerHex(args.HexCoord);
            Model.SkipTargetHexIllumination += (sender, args) => MainGui.OnSkipTargetHexIllumination(args.HexCoord);
            Model.SkipCurrentHexIllumination += (sender, args) => MainGui.OnSkipCurrentHexIllumination(args.HexCoord);           
        }
    }
}
