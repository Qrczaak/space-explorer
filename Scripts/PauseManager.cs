using UnityEngine;
using System.Collections;
using System;

//Class is responsible for action when tha game is paused
public class PauseManager : MonoBehaviour {

    private SaveSystem _saveSystem;

	// Use this for initialization
	void Start () {
        _saveSystem = GameObject.Find("_EconomicMechanism").GetComponent<SaveSystem>();
    }

    public void OnApplicationPause(bool paused)
    {
        //If game is paused, save the game
        if (paused)
        {
            _saveSystem.SaveGame();
        }
        //If game is unpaused load the game (with correct time)
        else
        {
            _saveSystem.LoadGame();
        }
    }
}
