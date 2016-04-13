using UnityEngine;
using System.Collections;

public class BackButtonEarth : MonoBehaviour {

    private ChangeScene _changeScene;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _changeScene = GameObject.Find("_SceneManager").GetComponent<ChangeScene>();
            _changeScene.BackToMainScene();
        }
    }
}
