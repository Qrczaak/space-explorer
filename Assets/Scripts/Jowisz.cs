using UnityEngine;
using System.Collections;

public class Jowisz : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnMouseUp()
    {
        Camera.main.backgroundColor = Color.gray;
        GameObject go = GameObject.Find("_EconomicMechanism");
        Economy ec = go.GetComponent<Economy>();
        ec.addMoney(5000);
        ec.UpdateResources();

    }
}
