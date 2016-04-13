using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DisplayerRocketLevel : MonoBehaviour {

    private RocketLevel _rocketLevel;

    public Text engineLevelText;
    public Text fuelTankLevelText;
    public Text storageLevelText;
    public Text materialsLevelText;

    public Text engineResourcesText;
    public Text fuelTankResourcesText;
    public Text storageResourcesText;
    public Text materialsResourcesText;

    private List<double> _listOfEngineResources;
    private List<double> _listOfFuelTankResources;
    private List<double> _listOfStorageResources;
    private List<double> _listOfMaterialsReources;

    // Use this for initialization
    void Start () {
        _rocketLevel = GameObject.Find("_EconomicMechanism").GetComponent<RocketLevel>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //  Function displays amount of materials on the screen.
    //  It takes the updated amount from RocketLevel class.
    public void displayText()
    {
        GetEngineResources();
        GetFuelTankResources();
        GetStorageResources();
        GetMaterialsResources();
        engineLevelText.text = "lvl. " + _rocketLevel.GetEngineLevel().ToString();
        fuelTankLevelText.text = "lvl. " + _rocketLevel.GetFuelTankLevel().ToString();
        storageLevelText.text = "lvl. " + _rocketLevel.GetStorageLevel().ToString();
        materialsLevelText.text = "lvl. " + _rocketLevel.GetMaterialsLevel().ToString();

        engineResourcesText.text = "Diamond: " + _listOfEngineResources[0] + " & Antimatter: " + _listOfEngineResources[1];
        fuelTankResourcesText.text = "Diamond: " + _listOfFuelTankResources[0] + " & Terbium: " + _listOfFuelTankResources[1];
        storageResourcesText.text = "Deutrium: " + _listOfStorageResources[0] + " & Antimatter: " + _listOfStorageResources[1];
        materialsResourcesText.text = "Deutrium: " + _listOfMaterialsReources[0] + " & Terbium: " + _listOfMaterialsReources[1];
    }

    //First Deuter, second Terb!
    private void GetMaterialsResources()
    {
        _listOfMaterialsReources = _rocketLevel.getMaterialsUpdateResources();
    }

    //First Diamond, second Antimatter!
    private void GetEngineResources()
    {
        _listOfEngineResources = _rocketLevel.getEngineUpdateResources();
    }

    //First Diamond, second Terb!
    private void GetFuelTankResources()
    {
        _listOfFuelTankResources = _rocketLevel.getFuelTankUpdateResources();
    }
    //First Deuter, second Antimatter!
    private void GetStorageResources()
    {
        _listOfStorageResources = _rocketLevel.getStorageUpdateResources();
    }

}


