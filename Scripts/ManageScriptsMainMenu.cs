using UnityEngine;
using System.Collections;

public class ManageScriptsMainMenu : MonoBehaviour {

    private GameObject _mainMenu;
    private GameObject _options; 
    private GameObject _instructions;
    private GameObject _credits;

    // Use this for initialization
    void Start () {
        _mainMenu = GameObject.Find("Canvas/PanelMainMenu");
        _options = GameObject.Find("Canvas/PanelOptions");
        _instructions = GameObject.Find("Canvas/PanelInstructions");
        _credits = GameObject.Find("Canvas/PanelCredits");
        turnOnOffInstructions(false);
        turnOnOffOptions(false);
        turnOnOffCredits(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void turnOnOffMainMenu(bool condition)
    {
        _mainMenu.SetActive(condition);
    }

    public void turnOnOffOptions(bool condition)
    {
        _options.SetActive(condition);
    }

    public void turnOnOffInstructions(bool condition)
    {
        _instructions.SetActive(condition);
    }

    public void turnOnOffCredits(bool condition)
    {
        _credits.SetActive(condition);
    }
}
