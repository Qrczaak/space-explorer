using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;

/* THIS CLASS IS VERY IMPORTANT, CALCULATE TIME, ADD RESOURCES
   Class responsible for counting Time. All missions are connected with
   Time and this class is responsible for counting that time. It also sends
   time to SaveSystem class to save the remaining time when the game is closing
   to know how much time left after turning on the game. This class also adds resources
   becuse this class knows when the time is over so sends information about resources
   that need to be added.
*/

public class MyTimer : MonoBehaviour
{
    private Economy _economyComponent;
    private GameObject _economyObject;
    private ResourceGenerator _resourceGenerator;

    public bool timeStart = false;
    private Text testTxt;
    private string _currentPlanetName;
    private Planet currentPlanet;

    private string stringTimeFromPlanet;
    private double parsedTime;
    public float floatTime;

    private bool currentChange = false;
    private int testCounter = 0;

    private String niceTime;

    //List to generate priority of resources addition
    private List<int> _randomList = new List<int> {1,2,3,4};
    private ArrayList _arrayRandomList = new ArrayList {1,2,3,4};


    private RocketLevel _rocket;
    public int numOfDiamond;
    public int numOfDeuter;
    public int numOfAntimatter;
    public int numOfTerb;
    public int flightCost;

    // Use this for initialization
    void Start()
    {
        testTxt = GameObject.Find("Canvas/PanelTimer/TextTimer").GetComponent<Text>();
        _economyObject = GameObject.Find("_EconomicMechanism");
        _economyComponent = _economyObject.GetComponent<Economy>();
        _rocket = _economyObject.GetComponent<RocketLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        //If timeStart - start then time is counting
        if (timeStart)
        {
            /*  IMPORTANT TO REMEMBER JAK BEDE ROBIL KOLEJNCE SCEY Z PLANETAMI
                Application.loadedLevelName != "MainScene" && 
                If the scene is changed text on which timer is displayed has to be read
                On both scenes the name of that text field has to be the same
                TextTimer! 
            */
            if (Application.loadedLevelName == "MainMenu")
            {

            }
            else
            {
                if (currentChange == false || testTxt == null)
                {

                    testTxt = GameObject.Find("Canvas/PanelTimer/TextTimer").GetComponent<Text>();
                    testCounter++;
                    currentChange = true;
                }

                //Amount of time decrease every "deltaTime" and then displayed
                floatTime -= Time.deltaTime;

                //Formats time to good shape
                FormatTime();

                //Display time on text field
                testTxt.text = niceTime;

                //If time is less than 0.5 (in practice equal to 0) timer is stopped and resources are added.
                if (floatTime < 0.5)
                {
                    timeStart = false;
                    addResources();

                }
            }
        }
    }

    public void ResetGame()
    {
        PlayerPrefs.SetInt("NumOfDiamond",0);
        PlayerPrefs.SetInt("NumOfDeuter",0);
        PlayerPrefs.SetInt("NumOfAntimatter",0);
        PlayerPrefs.SetInt("NumOfTerb",0);
        floatTime = 0;
    }

    // Formating time to display it
    private void FormatTime()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(floatTime);
        niceTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
    }

    /*  This function is used after pause or close a game.
        It is used to substract the amount of time thta game was turned off.
        It is called by SaveSystem class.
    */
    public void changeFloatTime(float minusTime)
    {
        if (timeStart)
        {
            //  If game was turned off more that the mission last then
            //  time is set to choosen amount of time (shouldn't be 0
            //  because then some problems occure but can be for example 0.1)
            if (minusTime > floatTime)
            {
                floatTime = 5;
            }
            else
            {
                floatTime = floatTime - minusTime;
            }
        }
    }

    public bool getTimeStart()
    {
        return timeStart;
    }

    public void setTimeStart(bool state)
    {
        timeStart = state;
    }

    public float getFloatTime()
    {
        return floatTime;
    }

    public void setFloatTime(float time)
    {
        floatTime = time;
    }

    public void SetCurrentPlanetName(string currentName)
    {
        _currentPlanetName = currentName;
        ChangeStringToInt();
    }

    // Changes string time gets from planet to float in seconds time
    private void ChangeStringToInt()
    {
        currentPlanet = GameObject.Find(_currentPlanetName).GetComponent<Planet>();
        stringTimeFromPlanet = currentPlanet.flightTime;
        floatTime = currentPlanet.getTimeInSeconds();
    }

    // If scene is change this function has to be used
    public void setChangeScene()
    {
        currentChange = false;
    }

    //Money are in class Condition Checker because must be reduced imidietly not after finish time
    public void addResources()
    {
        Camera.main.backgroundColor = Color.red;
        
        
        int currentCapacity;
        _randomList.Shuffle();

        /*
            Resources are generated earlier that is why amount of the has to be save in PlayerPrefs.
            Game can be crashed or turned off after generating the amount and the amount is needed
            only after the mission time is ended so they have to be save because in other case when
            the game is turned of during mission amount of resources is forget. Here we load
            amount of resources that was generated.
        */
        numOfDiamond = PlayerPrefs.GetInt("NumOfDiamond");
        numOfDeuter = PlayerPrefs.GetInt("NumOfDeuter");
        numOfAntimatter = PlayerPrefs.GetInt("NumOfAntimatter");
        numOfTerb = PlayerPrefs.GetInt("NumOfTerb");

        //Randomize which resource should be added first
        //It is helpfull because of the capacity
        //If the same resource would be first the same there would be 
        //a possibility to gain only that resource because of the fulfilling capacity
        for (int i = 0; i < 4; i++)
        {
            if(_randomList[i] == 1)
            {
                currentCapacity = _rocket.GetCurrentLoad();
                if(numOfDiamond <= currentCapacity)
                {
                    _economyComponent.addDiamond(numOfDiamond);
                    _rocket.SetCurrentLoad(numOfDiamond);
                }
                else
                {
                    int tempDiamond = numOfDiamond - currentCapacity;
                    _economyComponent.addDiamond(numOfDiamond - tempDiamond);
                    _rocket.SetCurrentLoad(numOfDiamond - tempDiamond);
                }
            }
            if (_randomList[i] == 2)
            {
                currentCapacity = _rocket.GetCurrentLoad();
                if (numOfDeuter <= currentCapacity)
                {
                    _economyComponent.addDeuter(numOfDeuter);
                    _rocket.SetCurrentLoad(numOfDeuter);
                }
                else
                {
                    int tempDeuter = numOfDeuter - currentCapacity;
                    _economyComponent.addDeuter(numOfDeuter - tempDeuter);
                    _rocket.SetCurrentLoad(numOfDeuter - tempDeuter);
                }
            }
            
            if (_randomList[i] == 3)
            {
                currentCapacity = _rocket.GetCurrentLoad();
                if (numOfAntimatter <= currentCapacity)
                {
                    _economyComponent.addAntimatter(numOfAntimatter);
                    _rocket.SetCurrentLoad(numOfAntimatter);
                }
                else
                {
                    int tempAntimatter = numOfAntimatter - currentCapacity;
                    _economyComponent.addAntimatter(numOfAntimatter - tempAntimatter);
                    _rocket.SetCurrentLoad(numOfAntimatter-tempAntimatter);
                }
            }
            if (_randomList[i] == 4)
            {
                currentCapacity = _rocket.GetCurrentLoad();
                if (numOfTerb <= currentCapacity)
                {
                    _economyComponent.addTerb(numOfTerb);
                    _rocket.SetCurrentLoad(numOfTerb);
                }
                else
                {
                    int tempTerb = numOfTerb - currentCapacity;
                    _economyComponent.addTerb(numOfTerb - tempTerb);
                    _rocket.SetCurrentLoad(numOfTerb-tempTerb);
                }
            }

        }
        _rocket.DeleteCurrentLoad();
        _economyComponent.UpdateResources();
    }

    
}

static class MyExtensions
{
    private static System.Random rng = new System.Random();
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
