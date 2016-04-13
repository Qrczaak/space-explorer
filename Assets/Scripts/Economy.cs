using UnityEngine;
using System.Collections;

/*  Class is responsible for keeping and processing all resources.
    Class also sets initial amount of resources 
*/

public class Economy : MonoBehaviour {

    //Actual amount of resources
    public float deuter = 500;
    public float diamond = 1000;
    public float antimatter = 250;
    public float terb = 125;
    public float money = 14000;

    //Reset amount of resources to
    private float _deuter = 500;
    private float _diamond = 1000;
    private float _antimatter = 250;
    private float _terb = 125;
    private float _money = 14000;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Getters and setters
    public float getMoney()
    {
        return money;
    }

    public void addMoney(int add)
    {
        money = money + add;
    }

    public float getDiamond()
    {
        return diamond;
    }

    public void addDiamond(int add)
    {
        diamond = diamond + add;
    }

    public float getDeuter()
    {
        return deuter;
    }

    public void addDeuter(int add)
    {
        deuter = deuter + add;
    }

    public float getAntimatter()
    {
        return antimatter;
    }

    public void addAntimatter(int add)
    {
        antimatter = antimatter + add;
    }

    public float getTerb()
    {
        return terb;
    }

    public void addTerb(int add)
    {
        terb = terb + add;
    }

    public void setMoney(float add)
    {
        money = add;
    }

    public void setDeuter(float add)
    {
        deuter = add;
    }

    public void setDiamond(float add)
    {
        diamond = add;
    }

    public void setAntimatter(float add)
    {
        antimatter = add;
    }

    public void setTerb(float add)
    {
        terb = add;
    }

    public void ResetGame()
    {
        money = _money;
        diamond = _diamond;
        deuter = _deuter;
        antimatter = _antimatter;
        terb = _terb;
        GameObject.Find("_EconomicMechanism").GetComponent<SaveSystem>().ResetGame();
    }

    //It is used to update resources on the screen after changes
    public void UpdateResources()
    {
        GameObject go = GameObject.Find("_SceneManager");
        ResourcesDisplayer rd = go.GetComponent<ResourcesDisplayer>();
        rd.UpdateResources();
    }
}
