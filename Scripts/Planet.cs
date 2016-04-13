using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class Planet : MonoBehaviour {

    private static TurnOnOffScripts _turnScripts;
    private bool firsttime = true;

    public string planetName;
    public int flightCost;
    public string flightTime;
    public int averageTemperature;
    public int distance;
    public List<string> listOfResources = new List<string>();


    //Amount of resources
    public int minDiamond;
    public int maxDiamond;
    public int minDeuter;
    public int maxDeuter;
    public int minAntimatter;
    public int maxAntimatter;
    public int minTerb;
    public int maxTerb;

    //Texts
    private Text textPlanetName;
    private Text textPlanetTemperature;
    private Text textFlightCost;
    private Text textFlightTime;
    private Text textResources;
    private Text textDistance;

    private GameObject go;
    private RocketLevel _rocketLevel;

    private double _parsedTime;
    private float _floatSecondTime;


    private float _reducedTimeInSeconds;
    private string _reducedTimeString;
    // Use this for initialization
    void Start () {

        //Panel assigned to variable
        assignTexts();
        _turnScripts = GameObject.Find("_SceneManager").GetComponent<TurnOnOffScripts>();
        _rocketLevel = GameObject.Find("_EconomicMechanism").GetComponent<RocketLevel>();
    }
	
	// Update is called once per frame
	void Update () {

        if (firsttime)
        {
            //Visible changed to false
            _turnScripts.turnOnOffPanel(false);
            firsttime = false;
        }

    }


    public void OnMouseUp()
    {
        //Input.GetTouch(0).fingerId
        if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            displayInformationOnPanel();
        
            //sends information of the planet to condition checker in _SceneManager
            sendInformationToConditionChecker();

            _turnScripts.turnOnOffPanel(true);

        }
    }

    /*
      Information about current clicked planet has to be
      send to the condition checker because class has to know
      which explore button is already clicked
   */
    private void sendInformationToConditionChecker()
    {
        ExploreConditionCheck ecc = GameObject.Find("_SceneManager").GetComponent<ExploreConditionCheck>();
        ecc.currentPlanetName = planetName;
    }

    private void displayInformationOnPanel()
    {
            parseTimeToSeconds();
            textPlanetName.text =  "Planet Name: " + planetName;
            textPlanetTemperature.text = "Average Temperature: " + averageTemperature.ToString();
            textFlightCost.text = "Flight Cost: " + flightCost.ToString();
            textDistance.text = "Distance: " + distance.ToString();
            displayFlightCost();
            displayResources();
    }


    /*Function is used to display flight time on Panel
      with reduced time because of engine level of the rocket.
      Here also time is assigne to values send to timer.
    */
    private void displayFlightCost()
    {
        //Timer is set from ConditionChecker
        if(_rocketLevel.GetEngineLevel() == 1)
        {
            _reducedTimeInSeconds = (float)(_floatSecondTime);
            TimeSpan timeSpan = TimeSpan.FromSeconds(_reducedTimeInSeconds);
            _reducedTimeString = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            textFlightTime.text = "Flight Time: " + _reducedTimeString;
        }
        else
        {
            double sqrt = Math.Pow(0.8, _rocketLevel.GetEngineLevel());
            _reducedTimeInSeconds = (float) (_floatSecondTime * sqrt);
            TimeSpan timeSpan = TimeSpan.FromSeconds(_reducedTimeInSeconds);
            _reducedTimeString = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            textFlightTime.text = "Flight Time: " + _reducedTimeString;
        }
    }

    private void parseTimeToSeconds()
    {
        _parsedTime = TimeSpan.Parse(flightTime).TotalSeconds;
        _floatSecondTime = (float)_parsedTime;
    }


    public float getTimeInSeconds()
    {
        return _reducedTimeInSeconds;
    }

    public string getTimeString()
    {
        return _reducedTimeString;
    }

    private void displayResources()
    {
        string toDisplay = "Resources:";
        int counter = 0;
        foreach(string a in listOfResources)
        {
            if (counter == 0)
            {
                toDisplay = toDisplay + " ";
            }
            else
            {
                toDisplay = toDisplay + " & ";
            }
            toDisplay = toDisplay + a;

            counter++;
        }

        textResources.text = toDisplay;
    }

    //Function used to assigns Texts on Panel to the texts here in a class
    private void assignTexts()
    {
        go = GameObject.Find("Canvas/PanelInfo/PanelPlanetName/TextPlanetName");
        textPlanetName = go.GetComponent<Text>();

        go = GameObject.Find("Canvas/PanelInfo/PanelPlanetTemp/TextPlanetTemp");
        textPlanetTemperature = go.GetComponent<Text>();

        go = GameObject.Find("Canvas/PanelInfo/PanelFlightCost/TextFlightCost");
        textFlightCost = go.GetComponent<Text>();

        go = GameObject.Find("Canvas/PanelInfo/PanelFlightTime/TextFlightTime");
        textFlightTime = go.GetComponent<Text>();

        go = GameObject.Find("Canvas/PanelInfo/PanelResources/TextResources");
        textResources = go.GetComponent<Text>();

        go = GameObject.Find("Canvas/PanelInfo/PanelDistance/TextDistance");
        textDistance = go.GetComponent<Text>();
    }

}
