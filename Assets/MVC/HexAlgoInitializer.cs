using Assets.HexGridAlgorithms;
using UnityEngine;
using Assets.MVC.Controllers;
using System.Collections;

namespace Assets.MVC
{
    public class HexAlgoInitializer : MonoBehaviour
    {     
        HexAlgoController _hexAlgoController;

        IEnumerator Start()
        {
            if (TerrainTextures.IsTexturesLoaded == false) TerrainTextures.LoadTextures();
            yield return new WaitForSeconds(1);
            _hexAlgoController = new HexAlgoController();
        }

    }
}

