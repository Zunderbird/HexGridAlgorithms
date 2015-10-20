using System;
using Assets.HexGridAlgorithms;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Buttons
{
    public class TerrainSelectionButton : MonoBehaviour
    {
        public TerrainTypes CurrentTerrainType;

        void Start()
        {
            TerrainTextures.LoadTextures();

            transform.GetChild(0).GetComponent<Text>().text = CurrentTerrainType.ToString();

            var input = gameObject.GetComponent<Button>();
            input.onClick.AddListener(NextTerrainType);
        }

        public void NextTerrainType()
        {
            if ((int) CurrentTerrainType + 1 >= Enum.GetNames(typeof (TerrainTypes)).Length)
                CurrentTerrainType = 0;
            else
            {
                CurrentTerrainType++;
            }

            MapsGenerator.MainTerrainType = CurrentTerrainType;
            transform.GetChild(0).GetComponent<Text>().text = CurrentTerrainType.ToString();
        }
    }
}

