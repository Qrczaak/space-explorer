using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConditionChecker : MonoBehaviour {

    private Planet planetComponent = null;
    private Economy _economyComponent = null;
    private GameObject _economyObject = null;
    private MyTimer _myTimer = null;
    private Text _textError = null;
    private TurnOnOffScripts _panelMagaer = null;
    private ResourceGenerator _resourceGenerator;
    private RocketLevel _rocket = null;

    private void Loader()
    {
        if (_rocket == null)
        {
            _rocket = GameObject.Find("_EconomicMechanism").GetComponent<RocketLevel>();
        }
        if(_economyObject == null)
        {
            _economyObject = GameObject.Find("_EconomicMechanism");
        }
        if (_panelMagaer == null)
        {
            _panelMagaer = GameObject.Find("_SceneManager").GetComponent<TurnOnOffScripts>();
        }

    }

    /*  It is main and most important function for allowing rocket to go on a mission.
        It checks almost all conditions (capacity is checking in different place) which has to 
        be fullfiled to go on mission. Money, Fuel Tank level and Materials level
    */
    public bool CheckConditions(string planetName)
    {
        //Loads necessary objects
        Loader();

        planetComponent = GameObject.Find(planetName).GetComponent<Planet>(); 
        _economyComponent = _economyObject.GetComponent<Economy>();
        _myTimer = _economyObject.GetComponent<MyTimer>();
        _resourceGenerator = _economyObject.GetComponent<ResourceGenerator>();

        //Check if player has enough money and the rocket is not on a mission already
        if (planetComponent.flightCost <= _economyComponent.getMoney() && _myTimer.timeStart == false)
        {
            //Check if distance is not too big - fuel tank level is enough
            if (_rocket.GetMaxDistance() >= planetComponent.distance)
            {
                //Check if the temperature on the planet is not too big or too small to land on it - materials have to be on enough level
                if(_rocket.GetMaximumTeperature() >= planetComponent.averageTemperature && _rocket.GetMinimumTemeperature() <= planetComponent.averageTemperature)
                {

                    _resourceGenerator.AddResources(planetName);
                    PlayerPrefs.SetString("PlanetName", planetName);
                    _myTimer.SetCurrentPlanetName(planetName);
                    _myTimer.timeStart = true;

                    return true;
                }
                else
                {
                    PanelTurnOn();
                    _textError.text = "Temperature on the planet is too dangerous for your rocket. You need to develop new materials";
                    return false;
                }
                
            }
            else
            {
                PanelTurnOn();
                _textError.text = "Planet is too far. You have to upgrade your Engine!";
                return false;
            }
        }
        else
        {
            PanelTurnOn();
            _textError.text = "You have not enough money! You can sell some resources in shop.";
            return false;
        }
    } 

    private void PanelTurnOn()
    {
        _panelMagaer.turnOnOffErrorPanel(true);
        if (_textError == null)
        {
            _textError = GameObject.Find("Canvas/PanelError/TextError").GetComponent<Text>();
        }
    }
}
