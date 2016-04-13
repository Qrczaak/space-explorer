using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class RocketAction : MonoBehaviour {

    private TurnOnOffScripts _manageScripts;
    private DisplayerRocketLevel _displayer;

	// Use this for initialization
	void Start () {
        _manageScripts = GameObject.Find("_SceneManager").GetComponent<TurnOnOffScripts>();
        _displayer = GameObject.Find("_SceneManager").GetComponent<DisplayerRocketLevel>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnMouseUp()
    {

        if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) { 
            _displayer.displayText();
        _manageScripts.turnOnOffRocketInfo(true);
        }
    }
}
