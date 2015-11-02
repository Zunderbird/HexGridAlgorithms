using Assets.MVC.Models;
using Assets.MVC.Views;
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
        }
    }
}
