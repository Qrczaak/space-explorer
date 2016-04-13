using UnityEngine;
using System.Collections;


/* That class is responsible for generate random amount of resources.
   It also save them to PlayerPrefs - it is explained below and in
   MyTimer class to which resources are send
*/
public class ResourceGenerator : MonoBehaviour {

    private Planet planetComponent = null;
    private MyTimer _myTimer = null;
    private Economy _economyComponent = null;
    private GameObject _economyObject = null;

    private int minDiamond;
    private int maxDiamond;
    private int minDeuter;
    private int maxDeuter;
    private int minAntimatter;
    private int maxAntimatter;
    private int minTerb;
    private int maxTerb;

    private int numOfDiamond;
    private int numOfDeuter;
    private int numOfAntimatter;
    private int numOfTerb;

    //  Reads min and max amount of specified resource that can be found on the planet
    private void GetMinMaxValues()
    {
        minDiamond = planetComponent.minDiamond;
        maxDiamond = planetComponent.maxDiamond;
        minDeuter = planetComponent.minDeuter;
        maxDeuter = planetComponent.maxDeuter;
        minAntimatter = planetComponent.minAntimatter;
        maxAntimatter = planetComponent.maxAntimatter;
        minTerb = planetComponent.minTerb;
        minTerb = planetComponent.maxTerb;
    }

    //  Generate random amount of resources
    private void HowMuchResources()
    {
   
        numOfDiamond = (int)AmountOfResource(minDiamond, maxDiamond);
        numOfDeuter = (int)AmountOfResource(minDeuter, maxDeuter);
        numOfAntimatter = (int)AmountOfResource(minAntimatter, maxAntimatter);
        numOfTerb = (int)AmountOfResource(minTerb, maxTerb);
    }

    private int ThrowDices()
    {
        int firstDice = Random.Range(1, 6);
        int secondDice = Random.Range(1, 6);
        int thirdDice = Random.Range(1, 6);

        return firstDice + secondDice + thirdDice;
    }

    private double AmountOfResource(int minResource, int maxResource)
    {
        int sumOfDices = ThrowDices();

        if (sumOfDices == 10 || sumOfDices == 11)
        {
            return (minResource + ((maxResource - minResource) *0.5));
        }
        else if(sumOfDices == 12)
        {
            return minResource + ((maxResource - minResource) * 0.65);
        }
        else if (sumOfDices == 13)
        {
            return minResource + ((maxResource - minResource) * 0.75);
        }
        else if (sumOfDices == 14 || sumOfDices == 15)
        {
            return minResource + ((maxResource - minResource) * 0.8);
        }
        else if (sumOfDices == 16 || sumOfDices == 17)
        {
            return minResource + ((maxResource - minResource) * 0.9);
        }
        else if (sumOfDices == 18)
        {
            return minResource + ((maxResource - minResource) * 1);
        }
        else if (sumOfDices == 9)
        {
            return minResource + ((maxResource - minResource) * 0.40);
        }
        else if (sumOfDices == 8)
        {
            return minResource + ((maxResource - minResource) * 0.35);
        }
        else if (sumOfDices == 7 || sumOfDices == 6)
        {
            return minResource + ((maxResource - minResource) * 0.3);
        }
        else if (sumOfDices == 5 || sumOfDices == 4)
        {
            return minResource + ((maxResource - minResource) * 0.2);
        }
        else if (sumOfDices == 3)
        {
            return minResource + ((maxResource - minResource) * 0);
        }
        return minResource;
    }

    //Adds resources - save them to PlayerPrefs and substract money
    public void AddResources(string planetName)
    {
        
        planetComponent = GameObject.Find(planetName).GetComponent<Planet>();
        _economyObject = GameObject.Find("_EconomicMechanism");
        _economyComponent = _economyObject.GetComponent<Economy>();
        _myTimer = _economyObject.GetComponent<MyTimer>();


        GetMinMaxValues();
        HowMuchResources();

        /*  
            Resources are added later after mission is finished but are generated here so 
            that is why amount of the has to be save in PlayerPrefs.
            Game can be crashed or turned off after generating the amount and the amount is needed
            only after the mission time is ended so they have to be save because in other case when
            the game is turned of during mission amount of resources is forget. Here we save
            that amount of resources.
        */
        PlayerPrefs.SetInt("NumOfDiamond", numOfDiamond);
        PlayerPrefs.SetInt("NumOfDeuter", numOfDeuter);
        PlayerPrefs.SetInt("NumOfAntimatter", numOfAntimatter);
        PlayerPrefs.SetInt("NumOfTerb", numOfTerb);

      
        _economyComponent.addMoney((planetComponent.flightCost) * (-1));
        _economyComponent.UpdateResources();
    }
}
