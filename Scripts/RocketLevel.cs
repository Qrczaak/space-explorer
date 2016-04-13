using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
Class is responsible for keeping rocket levels.
Updating (leveling up) rocket levels.
Calculating current possibilities of rocket (max distance, max capacity etc.).
Calculating amount of resources needed to update rocket.
Reducing amount of resources after update (with help of Economy class)
Sending information to diplayer which resources and how much to udpate.
*/
public class RocketLevel : MonoBehaviour {

    // Initial levles of rocket 
    private int _engineLevel = 1;
    private int _fuelTankLevel = 1;
    private int _storageLevel = 1;
    private int _materialsLevel = 1;

    private DisplayerRocketLevel _displayer = null;
    private Economy _economy = null;


    //Amounts of the resources need to update Engine at the beggining
    private double _diamondEngine = 1000;
    private double _antimatterEngine = 250;

    //Amount of resources need to update Fuel Tank
    private double _diamondFuelTank = 1000;
    private double _terbFuelTank = 125;

    //Amount of resources need to update Storage
    private double _deuterStorage = 500;
    private double _antimatterStorage = 250;

    //Amount of resources needed to update Materials
    private double _deuterMaterials = 500;
    private double _terbMaterials = 125;

    //Distance that can be flight with current fuel tank level
    private int _maxDistance = 50000;

    //Max Capacity of the rocket
    private int _maxCapacity = 3000;

    //How much is already occupied
    private int _currentLoad = 0;

    //Max and min temperature on earth
    private int _minTemperature = -100;
    private int _maxTemperature = 100;

    private static int _staticMaxTemp = 100;
    private static int _staticMinTemp = -100;
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void ResetRocketLevel()
    {
     _engineLevel = 1;
     _fuelTankLevel = 1;
     _storageLevel = 1;
     _materialsLevel = 1;
    }

    //  Get maximum temperature in which rocket can land (increases 50 degrees every materials update)
    public int GetMaximumTeperature()
    {
        return _maxTemperature = _staticMaxTemp + (50 * (_materialsLevel-1));
    }

    //  Get minimum temperature in which rocket can land (increases 50 degrees every materials update)
    public int GetMinimumTemeperature()
    {
        return _minTemperature = _staticMinTemp - (50 * (_materialsLevel-1));
    }

    // Rocket has capacity which is decreasing during loading resources - after misison it has to be set to 0
    public void DeleteCurrentLoad()
    {
        _currentLoad = 0;   
    }

    // Setting amount of resources that are loaded on rocket
    public void SetCurrentLoad(int temp)
    {
        _currentLoad += temp;
    }

    // Getting current load from rocket
    public int GetCurrentLoad()
    {
        return GetMaxCapacity() - _currentLoad;
    }

    // Get max capacity which is calculated in accordance to rocket storage level
    public int GetMaxCapacity()
    {

        return (int) (_maxCapacity * (Mathf.Pow(2, (_storageLevel-1))));
    }

    // Get max distance that can be flight by a rocket
    public int GetMaxDistance()
    {
        return _maxDistance * _fuelTankLevel;
    }

    // Group of setters used after loading a game
    public void SetEngineLevel(int level)
    {
        _engineLevel = level;
    }

    public void SetFuelTankLeve(int level)
    {
        _fuelTankLevel = level;
    }

    public void SetStorageLevel(int level)
    {
        _storageLevel = level;
    }

    public void SetMaterialsLevel(int level)
    {
        _materialsLevel = level;
    }

    private void loadDisplayer()
    {
        if(_displayer == null)
        {
            _displayer = GameObject.Find("_SceneManager").GetComponent<DisplayerRocketLevel>();
        }

        if(_economy == null)
        {
            _economy = GameObject.Find("_EconomicMechanism").GetComponent<Economy>();
        }
        
    }


    private void MaterialsUpdateCost()
    {
        _deuterMaterials = 500 * Mathf.Pow(2, (_materialsLevel - 1));
        _terbMaterials = 125 * Mathf.Pow(2, (_materialsLevel - 1));
    }

    //To Update Engine Deuter and Antimatter is necessary
    private void EngineUpdateCost()
    {

            _diamondEngine = 1000 * Mathf.Pow(2,(_engineLevel-1));
            _antimatterEngine = 250 * Mathf.Pow(2, (_engineLevel-1));
        
    }

    private void FuelTankUpdateCost()
    {
        _diamondFuelTank = 1000 * Mathf.Pow(2, (_fuelTankLevel - 1));
        _terbFuelTank = 125 * Mathf.Pow(2, (_fuelTankLevel - 1));
    }

    private void StorageUpdateCost()
    {
        _deuterStorage = 500 * Mathf.Pow(2, (_storageLevel - 1));
        _antimatterStorage = 250 * Mathf.Pow(2, (_storageLevel - 1));
    }

    //  Group of functions cheks if updates are possible.
    //  Checks if player has enough resources to make an update
    public bool IsMaterialsUpdatePossible()
    {
        loadDisplayer();
        
        //  Cost are updated - it means that cost are recalculated with respect to current rocket specification level
        MaterialsUpdateCost();

        // Comparing players reources taken from Economy class to resources needed 
        // calculated in this class and taken from this class (_deuterMaterials)
        if (_economy.getDeuter() >= _deuterMaterials)
        {
            if (_economy.getTerb() >= _terbMaterials)
            {
                ReduceResources(0, _deuterMaterials, 0, _terbMaterials);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }


    public bool IsStorageUpdatePossible()
    {
        loadDisplayer();
        StorageUpdateCost();

        if (_economy.getDeuter() >= _deuterStorage)
        {
            if (_economy.getAntimatter() >= _antimatterStorage)
            {
                ReduceResources(0, _deuterStorage, _antimatterStorage, 0);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }


    public bool IsEngineUpdatePossible()
    {
        loadDisplayer();
        
        if (_economy.getDiamond() >= _diamondEngine)
        {
            if (_economy.getAntimatter() >= _antimatterEngine)
            {
                ReduceResources(_diamondEngine, 0, _antimatterEngine, 0);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }  
    }

    public bool IsFuelTankUpdatePossible()
    {
        loadDisplayer();
        FuelTankUpdateCost();

        if(_economy.getDiamond() >= _diamondFuelTank)
        {
            if(_economy.getTerb() >= _terbFuelTank)
            {
                ReduceResources(_diamondFuelTank, 0, 0, _terbFuelTank);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
        
    }

    /*
        Group of funcions necessary to send to displayer amount and kind of resources that are 
        necessary to update specified device and dispay it on scereen.
    */

    //First diamond, second antimatter.
    public List<double> getEngineUpdateResources()
    {
        EngineUpdateCost();
        List <double> listOfEngineResources = new List<double>();
        listOfEngineResources.Add(_diamondEngine);
        listOfEngineResources.Add(_antimatterEngine);
        return listOfEngineResources;
    }

    //First deuter, second terb.
    public List<double> getMaterialsUpdateResources()
    {
        MaterialsUpdateCost();
        List<double> listOfMaterialsResources = new List<double>();
        listOfMaterialsResources.Add(_deuterMaterials);
        listOfMaterialsResources.Add(_terbMaterials);
        return listOfMaterialsResources;
    }

    //First diamond, second terb.
    public List<double> getFuelTankUpdateResources()
    {
        FuelTankUpdateCost();
        List<double> listOfFuelTankResources = new List<double>();
        listOfFuelTankResources.Add(_diamondFuelTank);
        listOfFuelTankResources.Add(_terbFuelTank);
        return listOfFuelTankResources;
    }

    //First deuter, second antimatter.
    public List<double> getStorageUpdateResources()
    {
        StorageUpdateCost();
        List<double> listOfStorageResources = new List<double>();
        listOfStorageResources.Add(_deuterStorage);
        listOfStorageResources.Add(_antimatterStorage);
        return listOfStorageResources;
    }


    // If update is possible resources are substracted
    private void ReduceResources(double _diamond, double _deuter, double _antimatter, double _terb)
    {
        _economy.addDiamond((int)_diamond * (-1));
        _economy.addDeuter((int)_deuter * (-1));
        _economy.addAntimatter((int)_antimatter * (-1));
        _economy.addTerb((int)_terb * (-1));

        _economy.UpdateResources();
    }

    public void updateLevelsOnScreen()
    {
        loadDisplayer();
        _displayer.displayText();
    }

    public int GetEngineLevel()
    {
        return _engineLevel;
    }

    public int GetFuelTankLevel()
    {
        return _fuelTankLevel;
    }

    public int GetStorageLevel()
    {
        return _storageLevel;
    }

    public int GetMaterialsLevel()
    {
        return _materialsLevel;
    }

    // Group of functions responisble for updating (leveling up) rocket
    public void UpdateEngineLevel()
    {
        _engineLevel++;
    }

    public void UpdateFuelTankLevel()
    {
        _fuelTankLevel++;
    }

    public void UpdateStorageLevel()
    {
        _storageLevel++;
    }

    public void UpdateMaterialsLevel()
    {
        _materialsLevel++;
    }
}
