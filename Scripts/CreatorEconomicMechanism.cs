using UnityEngine;
using System.Collections;

public class CreatorEconomicMechanism : MonoBehaviour {

    public GameObject EconomicMechanismObj;
    void Awake()
    {
        if (GameObject.Find("_EconomicMechanism") != true)
        {
            GameObject _Manager = Instantiate(EconomicMechanismObj);
            _Manager.name = "_EconomicMechanism";
        }
    }
	// Use this for initialization
	void Start () {


    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
