using UnityEngine;
using System.Collections;

public class InfoButtonAction : MonoBehaviour {

    private TurnOnOffScripts _scriptManager;

    private DisplayInformationInMoreInfo _displayer;
	// Use this for initialization
	void Start () {
        _scriptManager = GameObject.Find("_SceneManager").GetComponent<TurnOnOffScripts>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void turnOnInfoPanel()
    {
        _displayer = GameObject.Find("_SceneManager").GetComponent<DisplayInformationInMoreInfo>();
       _scriptManager.turnOnOffRocketInfo(false);
        _displayer.displayActualState();
       _scriptManager.turnOnOffRocketMorePanelInfo(true);
    }

    
    
    public void backButtonAction()
    {
        _scriptManager.turnOnOffRocketMorePanelInfo(false);
        _scriptManager.turnOnOffRocketInfo(true);
    }
}
