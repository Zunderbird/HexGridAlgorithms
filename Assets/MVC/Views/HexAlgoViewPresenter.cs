using Assets.Scripts;
using UnityEngine;

namespace Assets.MVC.Views
{
    class HexAlgoViewPresenter : BaseViewPresenter
    {
        private void Start()
        {
            CurrentCanvas = MedievalScreenCanvas.transform;
            HexMap.AddComponent<ActionsOnlyInCanvas>();
            HexMap.AddComponent<CameraMove>().PlayerCamera = MapsCamera;
            HexMap.AddComponent<MouseActionsCatcher>().PlayerCamera = MapsCamera;
        }
    }
}
