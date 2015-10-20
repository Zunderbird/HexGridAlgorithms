using System;
using Assets.HexGridAlgorithms;
using Assets.Scripts.InputFields;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Buttons
{
    public class CreateHexGridButton : MonoBehaviour
    {
        public GameObject InputFieldWidth;
        public GameObject InputFieldHeight;

        public GameObject TerrainTypeButton;

        public int MaxNumber = 300;

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
                if (inputField != null)
                {
                    InputFieldWidth.AddComponent<CorrectionToInputField>();
                    inputField.onValueChange.AddListener(SetWidth);
                }
            }

            if (InputFieldHeight != null)
            {
                var inputField = InputFieldHeight.GetComponent<InputField>();
                if (inputField != null)
                {
                    InputFieldHeight.AddComponent<CorrectionToInputField>();
                    inputField.onValueChange.AddListener(SetHeight);                   
                }
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
            _widthInHex = CorrectInputFieldText(arg, InputFieldWidth);
            CheckForInteractable();
        }

        void SetHeight(string arg)
        {
            _heightInHex = CorrectInputFieldText(arg, InputFieldHeight);
            CheckForInteractable();
        }

        void CheckForInteractable()
        {
            gameObject.GetComponent<Button>().interactable = (_widthInHex > 0 && _heightInHex > 0);
        }

        int CorrectInputFieldText(string arg, GameObject inputField)
        {
            var number = 0;

            if (arg.Length > 0)
            {
                var lastSymbol = arg[arg.Length - 1];

                if (!lastSymbol.IsDigit() || Convert.ToInt32(arg) > MaxNumber)
                {
                    arg = arg.Remove(arg.Length - 1);
                    inputField.GetComponent<InputField>().text = arg;  
                }
                number = (arg.Length > 0) ? Convert.ToInt32(arg):0;
            }
            return number;
        }
    }
}

