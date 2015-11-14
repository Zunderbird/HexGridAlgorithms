using Assets.MVC.Models;
using Assets.MVC.Views;

namespace Assets.MVC.Controllers
{
    internal class HexAlgoController : BaseController
    {
        private HexAlgoViewPresenter _hexAlgoGui;
        private HexAlgoModel _hexAlgoModel;

        public HexAlgoController(HexAlgoViewPresenter presenter, HexAlgoModel model) : base(presenter, model)
        {
            _hexAlgoGui = presenter;
            _hexAlgoModel = model;
        }

        new public void Start()
        {
            base.Start();
            Model.LoadMap(@"Assets/HexMaps/HexMap.json");
            Model.FindMapsCentreCoords();
        }
    }
}
