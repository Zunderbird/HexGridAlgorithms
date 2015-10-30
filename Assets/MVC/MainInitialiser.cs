using Assets.HexGridAlgorithms;
using UnityEngine;
using Assets.MVC.Controllers;

namespace Assets.MVC
{
    public class MainInitialiser : MonoBehaviour
    {
        
        MapsGenController _mapsGenController;

        void Start()
        {
            TerrainTextures.LoadTextures();

            _mapsGenController = new MapsGenController();
        }

    }
}

