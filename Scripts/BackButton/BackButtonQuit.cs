using UnityEngine;
using System.Collections;

public class BackButtonQuit : MonoBehaviour {

    private GameObject _exitPanel;
	// Use this for initialization
	void Start () {
        _exitPanel = GameObject.Find("Canvas/PanelExit");
        _exitPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _exitPanel.SetActive(true);
        }
    }

    public void QuitGame()
    {
        GameObject.Find("_EconomicMechanism").GetComponent<SaveSystem>().SaveGame();
        Application.Quit();
    }
}
