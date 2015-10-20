using Assets.MVC.Models;
using Assets.MVC.Views;
using UnityEngine;

namespace Assets.MVC.Controllers
{
    class Controller
    {
        private Model _model;

        private MainGuiViewPresenter _mainGui;

        public void ShowView()
        {
            _mainGui = GameObject.Find("MainGUI").GetComponent<MainGuiViewPresenter>();


        }

    }
}
