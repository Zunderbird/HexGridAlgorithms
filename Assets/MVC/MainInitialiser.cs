using UnityEngine;
using Assets.MVC.Controllers;

namespace Assets.MVC
{
    public class MainInitialiser : MonoBehaviour
    {

        Controller _controller;

        void Start()
        {
            _controller = new Controller();

            _controller.ShowView();
        }

    }
}

