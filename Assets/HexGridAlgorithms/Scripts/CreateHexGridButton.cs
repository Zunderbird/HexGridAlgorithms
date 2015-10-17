using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.HexGridAlgorithms.Scripts
{
    public class CreateHexGridButton : MonoBehaviour
    {
        public GameObject InputFieldWidth;
        public GameObject InputFieldHeight;
        public GameObject ScaleSlider;

        private GameObject _hexGrid;

        private int _widthInHex;
        private int _heightInHex;

        void Start()
        {
            gameObject.GetComponent<Button>().interactable = false;

            var input = gameObject.GetComponent<Button>();
            input.onClick.AddListener(CreateGrid);

            if (InputFieldWidth != null)
            {
                var inputField = InputFieldWidth.GetComponent<InputField>();
                inputField.onEndEdit.AddListener(SetWidth);
            }

            if (InputFieldHeight != null)
            {
                var inputField = InputFieldHeight.GetComponent<InputField>();
                inputField.onEndEdit.AddListener(SetHeight);
            }
        }

        void CreateGrid()
        {
            if (_hexGrid != null) Destroy(_hexGrid);
            _hexGrid = MapsGenerator.GenerateMap(_widthInHex, _heightInHex);

            _hexGrid.transform.localScale *= 0.5f;

        }

        void SetWidth(string arg)
        {
            _widthInHex = Convert.ToInt32(arg);
            CheckForInteractable();
        }

        void SetHeight(string arg)
        {
            _heightInHex = Convert.ToInt32(arg);
            CheckForInteractable();
        }

        void CheckForInteractable()
        {
            gameObject.GetComponent<Button>().interactable = (_widthInHex > 0 && _heightInHex > 0);
        }
    }
}

