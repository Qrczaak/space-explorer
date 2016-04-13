using UnityEngine;
using System.Collections;

/*  Class is mainly responsible for checking if current planet to explore is 
    Earth or other planet. It also creates condition checker in which almost
    all economical conditions are checked before sending a rocket.
*/
public class ExploreConditionCheck : MonoBehaviour {

    private Economy _economyComponent;
    private ConditionChecker _checker = new ConditionChecker();
    public string currentPlanetName;

    private GameObject _solarSystem;
    private GameObject _objectPanel;

    private GameObject _planetEarth;
    private TurnOnOffScripts _manageScripts;
	// Use this for initialization
	void Start () {
        _manageScripts = GameObject.Find("_SceneManager").GetComponent<TurnOnOffScripts>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //Name of the function should be - Explore
    public void testFunction()
    {
        CheckPlanet();
    }

    //Checking which planet is clicked
    private void CheckPlanet()
    {
        // Current planet name is send here from different class
        if (currentPlanetName == "Earth")
        {

            _manageScripts.turnOnOffPanel(false);
            _manageScripts.turnOnOffSolarSystem(false);
            _manageScripts.turnOnOffPlanetEarth(true);
            _manageScripts.turnOnOffPlanetaryBackground(false);
            _manageScripts.CameraMovement(false);
            Vector3 camPos = new Vector3(-11,0,-10);
            Camera.main.transform.position = camPos;
            Camera.main.orthographicSize = 13;
        }

        //  Current planet name is necessary to send to know which conditions have to be fulfilled
        else
        {
            _checker.CheckConditions(currentPlanetName);
        }
    }
}
