using Assets.HexGridAlgorithms;
using UnityEngine;
using Assets.MVC.Controllers;
using Assets.MVC.Models;
using Assets.MVC.Views;

namespace Assets.MVC
{
    public class HexAlgoInitializer : MonoBehaviour
    {     
        HexAlgoController _hexAlgoController;

        void Start()
        {
            // If second scene had not loaded after first scene
            // Load texture from the resource files

            if (TerrainTextures.IsTexturesLoaded == false) TerrainTextures.LoadTextures();

            // Model's and view's initialization

            var mainGui = GameObject.Find("MainGUI").GetComponent<HexAlgoViewPresenter>();
            var model = new HexAlgoModel();

            // Controller's initialization

            _hexAlgoController = new HexAlgoController(mainGui, model);

            // Controller starts working only after full view's initialization
            mainGui.Initialized += (sender, args) => _hexAlgoController.Start();
        }

    }
}

