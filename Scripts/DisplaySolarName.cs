using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplaySolarName : MonoBehaviour {

    private GameObject _currentSolarSystem;
    private SolarSystem _solarSystemScript;
    private string _sceneName;

    public Text solarSystemName;

	// Use this for initialization
	void Start () {

        _sceneName = Application.loadedLevelName;

        string newSceneName = _sceneName.Replace("Scene", "");
        _currentSolarSystem = GameObject.Find(newSceneName);
        _solarSystemScript = (SolarSystem) _currentSolarSystem.GetComponent(typeof(SolarSystem));

         
        
        DisplayName();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void DisplayName()
    {
        solarSystemName.text = _solarSystemScript.solarSystemName;
    }
}
