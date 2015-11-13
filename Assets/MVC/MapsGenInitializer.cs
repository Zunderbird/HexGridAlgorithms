using Assets.HexGridAlgorithms;
using UnityEngine;
using Assets.MVC.Controllers;
using Assets.MVC.Models;
using Assets.MVC.Views;

namespace Assets.MVC
{
    public class MapsGenInitializer : MonoBehaviour
    {
        
        MapsGenController _mapsGenController;

        void Start()
        {
            // Load texture from the resource files
            TerrainTextures.LoadTextures();

            // Model's and view's initialization

            var mainGui = GameObject.Find("MainGUI").GetComponent<MapsGenViewPresenter>();
            var model = new MapsGenModel();

            // Controller's initialization

            _mapsGenController = new MapsGenController(mainGui, model);

            // Controller starts working
            _mapsGenController.Start();
        }

    }
}

