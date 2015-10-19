using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.HexGridAlgorithms.Scripts
{
    public class TerrainSelectionButton : MonoBehaviour
    {
        public TerrainTypes CurrentTerrainType;

        void Start()
        {
            TerrainTextures.LoadTextures();

            transform.GetChild(0).GetComponent<Text>().text = CurrentTerrainType.ToString();

            var input = gameObject.GetComponent<Button>();
            if (input != null) input.onClick.AddListener(NextTerrainType);
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

