using Assets.Scripts;
using UnityEngine;

namespace Assets.MVC.Views
{
    class HexAlgoViewPresenter : BaseViewPresenter
    {
        private void Start()
        {
            CurrentCanvas = InternalScreenCanvas.transform;
            HexMap.AddComponent<ActionsOnlyInCanvas>();
            HexMap.AddComponent<CameraMove>().PlayerCamera = MapsCamera;
            HexMap.AddComponent<MouseActionsCatcher>().PlayerCamera = MapsCamera;

            OnInitialized();
        }
    }
}
