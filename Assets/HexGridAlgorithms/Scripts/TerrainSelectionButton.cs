using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.HexGridAlgorithms.Scripts
{
    public class TerrainSelectionButton : MonoBehaviour
    {
        public TerrainTypes CurrentTerrainType = TerrainTypes.Plain;

        void Start()
        {
            transform.GetChild(0).GetComponent<Text>().text = CurrentTerrainType.ToString();

            var input = gameObject.GetComponent<Button>();
            input.onClick.AddListener(NextTerrainType);
        }

        public void NextTerrainType()
        {
            if ((int)CurrentTerrainType + 1 >= Enum.GetNames(typeof(TerrainTypes)).Length)
                CurrentTerrainType = TerrainTypes.Plain;
            else
            {
                CurrentTerrainType++;
            }

            transform.GetChild(0).GetComponent<Text>().text = CurrentTerrainType.ToString();
        }
    }
}

