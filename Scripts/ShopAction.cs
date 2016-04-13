using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ShopAction : MonoBehaviour {

    private TurnOnOffScripts _manageScripts;



    // Use this for initialization
    void Start () {
        _manageScripts = GameObject.Find("_SceneManager").GetComponent<TurnOnOffScripts>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) { 
            _manageScripts.turnOnOffShopInfo(true);
    }
    }
}
