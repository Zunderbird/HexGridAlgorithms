using Assets.MVC.Models;
using Assets.MVC.Views;

namespace Assets.MVC.Controllers
{
    class MapsGenController : BaseController
    {
        private readonly MapsGenModel _mapsGenModel;

        private readonly MapsGenViewPresenter _mapsGenGui;

        public MapsGenController(MapsGenViewPresenter presenter, MapsGenModel model) : base(presenter, model)
        {
            _mapsGenGui = presenter;
            _mapsGenModel = model;
        }

        new public void Start()
        {
            base.Start();

            //View's events:
            //View's buttons

            _mapsGenGui.CreateHexMapButton.onClick.AddListener(_mapsGenModel.CreateHexMap);
            _mapsGenGui.TerrainTypeButton.onClick.AddListener(_mapsGenModel.SetNextTerrainType);
            _mapsGenGui.CentreMapButton.onClick.AddListener(_mapsGenModel.FindMapsCentreCoords);
            _mapsGenGui.SaveButton.onClick.AddListener(_mapsGenModel.SaveMap);
            _mapsGenGui.NextButton.onClick.AddListener(_mapsGenModel.NextStage);

            //View's Input fields

            _mapsGenGui.MapWidthInputField.onValueChange.AddListener(_mapsGenModel.SetWidthInHex);
            _mapsGenGui.MapHeightInputField.onValueChange.AddListener(_mapsGenModel.SetHeightInHex);

            //Model's events:

            _mapsGenModel.TerrainTypeWasSet += (sender, args) => _mapsGenGui.OnTerrainTypeWasSet(_mapsGenModel.CurrentTerrainType);
            _mapsGenModel.WidthCorrected += (sender, args) => _mapsGenGui.OnWidthCorrected(_mapsGenModel.MapWidthInHex);
            _mapsGenModel.HeightCorrected += (sender, args) => _mapsGenGui.OnHeightCorrected(_mapsGenModel.MapHeightInHex);

            _mapsGenModel.DeleteHexMap += (sender, args) => _mapsGenGui.OnDeleteHexMap();
            _mapsGenModel.CreationMapsAdmissible += (sender, args) => _mapsGenGui.OnCreationMapsAdmissible(true);
            _mapsGenModel.CreationMapsInadmissible += (sender, args) => _mapsGenGui.OnCreationMapsAdmissible(false);

            _mapsGenModel.PaintHex += (sender, args) => _mapsGenGui.OnPaintHex(args.HexCoord, args.HexType);
            _mapsGenModel.UpdateDistance += (sender, args) => _mapsGenGui.OnUpdateDistance(args.Text);

            _mapsGenModel.LoadingNextStage += (sender, args) => _mapsGenGui.OnLoadingNextStage();
        }
    }
}
