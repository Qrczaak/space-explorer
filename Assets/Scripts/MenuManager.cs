using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    private ManageScriptsMainMenu _scriptManager;
	// Use this for initialization
	void Start () {
        _scriptManager = GameObject.Find("_SceneManager").GetComponent<ManageScriptsMainMenu>();
	}

    public void OptionsPressed()
    {
        _scriptManager.turnOnOffMainMenu(false);
        _scriptManager.turnOnOffOptions(true);
    }

    public void InstructionsPressed()
    {
        _scriptManager.turnOnOffMainMenu(false);
        _scriptManager.turnOnOffInstructions(true);
    }

    public void CreditsPressed()
    {
        _scriptManager.turnOnOffMainMenu(false);
        _scriptManager.turnOnOffCredits(true);
    }
    public void BackPressed()
    {
        _scriptManager.turnOnOffInstructions(false);
        _scriptManager.turnOnOffOptions(false);
        _scriptManager.turnOnOffCredits(false);
        _scriptManager.turnOnOffMainMenu(true);
    }

    public void ExitPressed()
    {
        GameObject.Find("_EconomicMechanism").GetComponent<SaveSystem>().SaveGame();
        Application.Quit();
    }

}
