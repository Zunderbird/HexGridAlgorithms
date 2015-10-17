﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.HexGridAlgorithms.Scripts
{
    public class InputFieldController : MonoBehaviour
    {
        public int MaxNumber = 300;

        void Start()
        {
            var input = gameObject.GetComponent<InputField>();
            input.onValueChange.AddListener(CheckInputSymbol);
        }

        void CheckInputSymbol(string arg)
        {
            if (arg.Length > 0)
            {
                var lastSymbol = arg[arg.Length - 1];

                if (!lastSymbol.IsDigit() || Convert.ToInt32(arg) > MaxNumber)
                    gameObject.GetComponent<InputField>().text = arg.Remove(arg.Length - 1);
            }
        }
    }
}


