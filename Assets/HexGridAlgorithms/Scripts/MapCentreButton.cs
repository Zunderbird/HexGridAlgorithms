using UnityEngine;
using UnityEngine.UI;

public class MapCentreButton : MonoBehaviour
{

	void Start ()
    {
        var input = gameObject.GetComponent<Button>();
        if (input != null) input.onClick.AddListener(PlaceCameraToMapCentre);
    }

	void Update ()
    {
	
	}

    void PlaceCameraToMapCentre()
    {
        
    }
}
