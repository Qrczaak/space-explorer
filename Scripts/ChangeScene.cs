using UnityEngine;
using System.Collections.Generic;
using System.Collections;


/*
    This class is responsible mainly for loading new scene
    After pressing ExploreButton class has to decide which 
    solar system was turned on - chooseProperScene function
    is responsible for that. This class is also responsible 
    for sending _Mangager and Solar System to the next scene.
*/
public class ChangeScene : MonoBehaviour {
    
    //variable send from the actually pressed solarsystem
    private string _sceneManager;
    private List<GameObject> _listOfsolarSystemsObject = new List<GameObject>();
    private SolarSystem _solarSystemScript;

    //Object of Solar System
    private GameObject solarSystemObject;
    private GameObject solarSystemObjectPlanets;
    
    //To disable renderd of sprite in new scene (because its not necessary)
    private SpriteRenderer _spriteVisible;

    //To disable possibility of click of sprite in new scene (because its not necessary)
    private BoxCollider2D _boxCollider;

    private GameObject _earthSystemObject;

    private TurnOnOffScripts _manageScripts;


    private AudioSource _audioSource;
    public AudioClip buttonExplore;

    private GameObject _errorrPanel;
    void Start()
    {
        _manageScripts = GameObject.Find("_SceneManager").GetComponent<TurnOnOffScripts>();
        _errorrPanel = GameObject.Find("Canvas/PanelErrorPlanetary");
        _errorrPanel.SetActive(false);
        _audioSource = this.GetComponent<AudioSource>();
        // TO VERIFY IF FIRST RUN OF THE GAME IF YES MAIN MENU DISPLAYED
        if (PlayerPrefs.GetInt("MainMenuTruee") == 1)
        {
        }
        else
        {
            PlayerPrefs.SetInt("MainMenuTruee", 1);
            ChangeToMainMenuScene();
        }
    }

    IEnumerator WaitForSound()
    {
        Debug.Log("Before Waiting 2 seconds");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("After Waiting 2 Seconds");
        //Write all solar names int list 
        readAllSolarNames();

        //Send _Manager to next scene - _Mangager is responsible for all all economic mechanism
        sendEconomicManagerToNextScene();

        //IMPORTANT - THIS IF IS MADE ONLY BECAUSE THERE ARE MORE THAN ONE SOLAR SYSTEM BUT ONLY
        //EARTH SOLAR SYSTEM IS FINISHED SO ONLY THIS ONE CAN BE LOADED
        //(_sceneManager is a name of proper scene to load sends from SolarSystem class)
        if (_sceneManager == "Earth Solar System")
        {
            //Iterate throught list to find proper scene to run
            chooseProperSceneToLoad();
        }
        if (_sceneManager == "Kepler-90")
        {
            _errorrPanel.SetActive(true);
        }
    }

    public void ChangeSceneTo(int changeScene)
    {
        if (PlayerPrefs.GetInt("StopMusic") == 0)
        {
            StartCoroutine(WaitForSound());
            _audioSource.PlayOneShot(buttonExplore);
            
        }
        

    }

    
    //TO VERIFY IF WORKS LATER
    public void BackToMainScene()
    {
        //Check which scene is run
        //If Solar System is on
        if (Application.loadedLevelName == "EarthSolarSystemScene")
        {
            if (_manageScripts.getSolarSystemStatus())
            {
                _manageScripts.CameraMovement(true);
                string a = Application.loadedLevelName;
                string res = a.Replace("Scene", "");
                GameObject newobj = GameObject.Find(res);
                Object.Destroy(newobj);
                Application.LoadLevel("MainScene");
                MyTimer mt = GameObject.Find("_EconomicMechanism").GetComponent<MyTimer>();
                mt.setChangeScene();
            }
            else
            {
                _manageScripts.CameraMovement(true);
                _manageScripts.turnOnOffPlanetaryBackground(true);
                Object.DontDestroyOnLoad(GameObject.Find("Rocket"));
                _manageScripts.turnOnOffPlanetEarth(false);
                _manageScripts.turnOnOffSolarSystem(true);

            }
        }
        //If Main Menu is on
        else if(Application.loadedLevelName == "MainMenu")
        {
            Application.LoadLevel("MainScene");
        }
    }

    public void ChangeToMainMenuScene()
    {
        sendEconomicManagerToNextScene();
        Application.LoadLevel("MainMenu");
    }

    private void sendEconomicManagerToNextScene()
    {
        Object.DontDestroyOnLoad(GameObject.Find("_EconomicMechanism"));
    }


    private void chooseProperSceneToLoad()
    {
        //Iterate throught whole list of solar system names
        for (int i = 0; i < _listOfsolarSystemsObject.Count; i++)
        {
            //Getting component of SolarSystem to get name
            _solarSystemScript = (SolarSystem) _listOfsolarSystemsObject[i].GetComponent(typeof(SolarSystem));
            
            //Checks first possibility - if the earth solar system is pressed.
            //_sceneManager is the name of the pressed SolarSystem - send from SolarSystem class.
            if (_sceneManager == _solarSystemScript.solarSystemName)
            {
                //Name must be the same in Inspector
                string result = _sceneManager.Replace(" ", "");

                //It is necessary to send object to next scene
                solarSystemObject = _listOfsolarSystemsObject[i];

                DisableVisibleOfSprite();
                Object.DontDestroyOnLoad(solarSystemObject);

                Application.LoadLevel(_listOfsolarSystemsObject[i].name + "Scene");
                MyTimer mt = GameObject.Find("_EconomicMechanism").GetComponent<MyTimer>();
                mt.setChangeScene();
            }
        }
    }


    private void readAllSolarNames()
    {
        _listOfsolarSystemsObject.Add(GameObject.Find("EarthSolarSystem"));


        _listOfsolarSystemsObject.Add(GameObject.Find("OtherSolarSystem"));


        _listOfsolarSystemsObject.Add(GameObject.Find("Other2SolarSystem"));

    }

    public string getSceneManager()
    {
        return _sceneManager;
    }

    //Name of the solar system send from SolarSystem class
    public void setSceneManager(string sceneManager)
    {
        _sceneManager = sceneManager;
    }


    /*
    Function used to disable to visibility of the sprite, because changing scene to the next one we send the object
    (solar system) together with render and collider, it has to be disabled to be invisible. Later when player come back
    to main scene it has to be enabled to be visible again.
    */
    private void DisableVisibleOfSprite()
    {
        solarSystemObjectPlanets = solarSystemObject.transform.GetChild(0).gameObject;

        _spriteVisible = (SpriteRenderer)solarSystemObject.GetComponent(typeof(SpriteRenderer));
        _boxCollider = (BoxCollider2D)solarSystemObject.GetComponent(typeof(BoxCollider2D));
        _spriteVisible.enabled = false;
        _boxCollider.enabled = false;

        solarSystemObjectPlanets.SetActive(false);
    }
}

