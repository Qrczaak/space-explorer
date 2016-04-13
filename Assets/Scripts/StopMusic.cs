using UnityEngine;
using System.Collections;

public class StopMusic : MonoBehaviour {

    AudioSource _audio;

    void Start()
    {
        _audio = GameObject.Find("_EconomicMechanism").GetComponent<AudioSource>();
    }
	// Update is called once per frame
	void Update () {
	
	}

    public void MusicOff()
    {
        if (PlayerPrefs.GetInt("StopMusic") == 0)
        {
                PlayerPrefs.SetInt("StopMusic", 1);
                _audio.enabled = false;
        }
        else
        {
                PlayerPrefs.SetInt("StopMusic", 0);
                _audio.enabled = true;
        }
    }
}
