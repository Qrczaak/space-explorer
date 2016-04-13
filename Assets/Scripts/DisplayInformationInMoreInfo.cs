using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/* This class is used to display information about the Rocket.
    Current max capacity, max distanc which rocket can fly and
    max and min temperature in which can land. It is displayed
    on Earth planet - after clicking on rocket and after clicking
    on "More" on the panel.
*/
public class DisplayInformationInMoreInfo : MonoBehaviour {

    public Text actualCapacityText;
    public Text actualMaxDistanceText;
    public Text actualMinAndMaxText;

    private RocketLevel _rocketLevel;
	// Use this for initialization
	void Start () {
        _rocketLevel = GameObject.Find("_EconomicMechanism").GetComponent<RocketLevel>();
	}
	
	//Function responsible for displaying information
    public void displayActualState()
    {
        actualCapacityText.text = "Max Capacity: " + _rocketLevel.GetMaxCapacity();
        actualMaxDistanceText.text = "Max distance: " + _rocketLevel.GetMaxDistance();
        actualMinAndMaxText.text = "Max temp: " + _rocketLevel.GetMaximumTeperature() + " & Min temp: " + _rocketLevel.GetMinimumTemeperature();
    }
}
