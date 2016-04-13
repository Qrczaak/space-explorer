using UnityEngine;
using System.Collections;

public class UpdateButtons : MonoBehaviour {

    private RocketLevel _rocketLevel;
	// Use this for initialization
	void Start () {
        _rocketLevel = GameObject.Find("_EconomicMechanism").GetComponent<RocketLevel>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateEngine()
    {
        if (_rocketLevel.IsEngineUpdatePossible())
        {
            _rocketLevel.UpdateEngineLevel();
            _rocketLevel.updateLevelsOnScreen();
        }
    }

    public void UpdateFuelTank()
    {
        if (_rocketLevel.IsFuelTankUpdatePossible())
        {
            _rocketLevel.UpdateFuelTankLevel();
            _rocketLevel.updateLevelsOnScreen();
        }
    }

    public void UpdateStorage()
    {
        if (_rocketLevel.IsStorageUpdatePossible())
        {
            _rocketLevel.UpdateStorageLevel();
            _rocketLevel.updateLevelsOnScreen();
        }
    }

    public void UpdateMaterials()
    {
        if (_rocketLevel.IsMaterialsUpdatePossible())
        {
            _rocketLevel.UpdateMaterialsLevel();
            _rocketLevel.updateLevelsOnScreen();
        }     
    }
}
