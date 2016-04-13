using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResourcesDisplayer : MonoBehaviour {


    public Text textMoney;
    public Text textDiamond;
    public Text textDeuter;
    public Text textAntimatter;
    public Text textTerb;

    private GameObject EconomicSystemObj;
    private Economy economyStatus;
	// Use this for initialization
	void Start () {
        EconomicSystemObj = GameObject.Find("_EconomicMechanism");
        economyStatus = EconomicSystemObj.GetComponent<Economy>();

        textMoney.text = "Money:\n" + economyStatus.getMoney();
        textDiamond.text = "Diamond:\n" + economyStatus.getDiamond();
        textDeuter.text = "Deutrium:\n" + economyStatus.getDeuter();
        textAntimatter.text = "Antimatter:\n" + economyStatus.getAntimatter();
        textTerb.text = "Terbium:\n" + economyStatus.getTerb();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateResources()
    {
        EconomicSystemObj = GameObject.Find("_EconomicMechanism");
        economyStatus = EconomicSystemObj.GetComponent<Economy>();

        textMoney.text = "Money:\n" + economyStatus.getMoney();
        textDiamond.text = "Diamond:\n" + economyStatus.getDiamond();
        textDeuter.text = "Deutrium:\n" + economyStatus.getDeuter();
        textAntimatter.text = "Antimatter:\n" + economyStatus.getAntimatter();
        textTerb.text = "Terbium:\n" + economyStatus.getTerb();
    }
}
