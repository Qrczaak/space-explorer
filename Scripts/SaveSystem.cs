using UnityEngine;
using System.Collections;
using System;

/*
Class is responsible for saving and loading a game
    */
public class SaveSystem : MonoBehaviour {

    private Economy _economyManager = null;
    private GameObject _economicMechanismObject = null;
    private MyTimer _timer = null;
    private RocketLevel _rocket = null;

    private DateTime _logoutTime;

    private float _timeToSave = 0;


        // Use this for initialization
	void Start () {
        // Loads game every time game is started
        LoadGame();
    }
	
	// Update is called once per frame
    // Here update is used to save every 30 seconds
	void Update () {
        _timeToSave += Time.deltaTime;
        if(_timeToSave > 30)
        {
            SaveGame();
            _timeToSave = 0;
        }
    }

    public void ResetGame()
    {
        _timer.ResetGame();
        SaveGame();
    }
    // Saving game
    public void SaveGame()
    {
        readGameObjects();

        // Save resources and rocket devices levels
        PlayerPrefs.SetFloat("Money", _economyManager.getMoney());
        PlayerPrefs.SetFloat("Diamond", _economyManager.getDiamond());
        PlayerPrefs.SetFloat("Deuter", _economyManager.getDeuter());
        PlayerPrefs.SetFloat("Antimatter", _economyManager.getAntimatter());
        PlayerPrefs.SetFloat("Terb", _economyManager.getTerb());

        PlayerPrefs.SetInt("Engine", _rocket.GetEngineLevel());
        PlayerPrefs.SetInt("FuelTank", _rocket.GetFuelTankLevel());
        PlayerPrefs.SetInt("Storage", _rocket.GetStorageLevel());
        PlayerPrefs.SetInt("Materials", _rocket.GetMaterialsLevel());

        // Save logout time to calculate the difference in time after turn on the game and load
        _logoutTime = DateTime.Now;

        // Changin date from DateTime to String
        string logoutTime = _logoutTime.ToString();

        // Save time
        PlayerPrefs.SetString("LogoutTime", logoutTime);

        bool state;
        
        // Cheks if the mission was started or not
        state = _timer.getTimeStart();

        //  If mission was started saves 1 if not saves 0
        //  It is necessary when game is loaded to know if time calulation is necessary
        if (state)
        {
            PlayerPrefs.SetInt("TimerOn", 1);
        }
        else
        {
            PlayerPrefs.SetInt("TimerOn", 0);
        }

        // Saves current left time of the mission
        PlayerPrefs.SetFloat("FloatTime", _timer.getFloatTime());

        //SaveGame is used it means that it is not the first run of the game
        PlayerPrefs.SetInt("IsFirstRun", 1);
    }

    public void LoadGame()

    {
        readGameObjects();
        int checkState = PlayerPrefs.GetInt("IsFirstRun");

        // Cheks if it is the first run of the game - if it is game is not loaded
        if(checkState != 0)
        {
            // Loads resources and rocked devices levels
            _economyManager.setMoney(PlayerPrefs.GetFloat("Money"));
            _economyManager.setDiamond(PlayerPrefs.GetFloat("Diamond"));
            _economyManager.setDeuter(PlayerPrefs.GetFloat("Deuter"));
            _economyManager.setAntimatter(PlayerPrefs.GetFloat("Antimatter"));
            _economyManager.setTerb(PlayerPrefs.GetFloat("Terb"));

            _rocket.SetEngineLevel(PlayerPrefs.GetInt("Engine"));
            _rocket.SetFuelTankLeve(PlayerPrefs.GetInt("FuelTank"));
            _rocket.SetStorageLevel(PlayerPrefs.GetInt("Storage"));
            _rocket.SetMaterialsLevel(PlayerPrefs.GetInt("Materials"));

            // Load logout time
            string logoutString = PlayerPrefs.GetString("LogoutTime");
            
            // Parse string time to DateTime type
            DateTime logoutTime = DateTime.Parse(logoutString);

            // Gets current time
            DateTime currentTime = DateTime.Now;

            // Calculates difference in time between logout and login
            TimeSpan timeDifference = currentTime - logoutTime;

            // Parsed time to float and to seconds
            float parsedTime = (float)timeDifference.TotalSeconds;

            int state = PlayerPrefs.GetInt("TimerOn");

            // If mission was started time is substracted and mission is going on.
            if (state == 1)
            {
                _timer.setTimeStart(true);
                _timer.setFloatTime(PlayerPrefs.GetFloat("FloatTime"));
                _timer.changeFloatTime(parsedTime);
            }
            else
            {
                _timer.setTimeStart(false);
            }


            _economyManager.UpdateResources();
        }
        
    }

    private void readGameObjects()
    {
        if(_economicMechanismObject == null)
        {
            _economicMechanismObject = GameObject.Find("_EconomicMechanism");
        }

        if(_economyManager == null)
        {
            _economyManager = _economicMechanismObject.GetComponent<Economy>();
        }
        if(_timer == null)
        {
            _timer = _economicMechanismObject.GetComponent<MyTimer>();
        }
        if (_rocket == null)
        {
            _rocket = _economicMechanismObject.GetComponent<RocketLevel>();
        }
    }
}
