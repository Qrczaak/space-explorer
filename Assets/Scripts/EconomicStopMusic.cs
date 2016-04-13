using UnityEngine;
using System.Collections;

public class EconomicStopMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    if(PlayerPrefs.GetInt("StopMusic") == 1)
        {
            this.GetComponent<AudioSource>().enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
