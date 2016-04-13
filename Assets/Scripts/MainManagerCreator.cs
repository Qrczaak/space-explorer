using UnityEngine;
using System.Collections;

public class MainManagerCreator : MonoBehaviour {

    public GameObject _newObject;

    void Awake()
    {
        
        GameObject _Manager = Instantiate(_newObject);
        _Manager.name = "_Manager";
    }
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
