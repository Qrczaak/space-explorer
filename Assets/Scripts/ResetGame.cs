using UnityEngine;
using System.Collections;
using System;

public class ResetGame : MonoBehaviour {

    public void ResetTheGame()
    {

            GameObject.Find("_EconomicMechanism").GetComponent<Economy>().ResetGame();
            GameObject.Find("_EconomicMechanism").GetComponent<RocketLevel>().ResetRocketLevel();
        
    }

}
