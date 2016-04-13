using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SolarSystem : MonoBehaviour
{
    int counter = 2;

    private static GameObject _objectPanel;
    private bool firsttime = true;
    private GameObject _objectSolarSystemName;

    //It is use to run appriopriate scene using button
    private GameObject _objectSceneManager;

    public string solarSystemName;
    public int numberOfPlantes;
    public int distanceFromEarth;

    public Text textSolarSystemName;
    public Text textNumberOfPlanets;
    public Text textInfo;
    
    public string info;

    private AudioSource _audioSource;
    public AudioClip soundEffect;

    //private Text _textSolarSystemName;

    // Use this for initialization
    void Start()
    {
        //Panel assigned to variable
        _objectPanel = GameObject.Find("Canvas/PanelInfo");
        _audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (firsttime)
        {
            //Visible changed to false
            _objectPanel.SetActive(false);
            firsttime = false;
        }
    }


    public void OnMouseUp()
    {
        //Input.GetTouch(0).fingerId
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if(PlayerPrefs.GetInt("StopMusic") == 0)
            {
                _audioSource.PlayOneShot(soundEffect);
            }
            displayInformationOnPanel();
           
            //Send name of actual solar system to the _Manager
            prepareToChangeScene();
            _objectPanel.SetActive(true);

        }
    }



    private void displayInformationOnPanel()
    {
        textSolarSystemName.text = "Solar System: " + solarSystemName;
        textNumberOfPlanets.text = "Number of Planets: " + numberOfPlantes;
        textInfo.text = "Info: " + info;
    }

    /*
        This function is used to send to _Manger's component - ChangeScene - name of 
        the solar system that is already clicked.
        It gives an opportunity to run appriorpiate scene using "explore" button.
    */
    private void prepareToChangeScene()
    {
        _objectSceneManager = GameObject.Find("_SceneManager");
        ChangeScene csScript = (ChangeScene)_objectSceneManager.GetComponent(typeof(ChangeScene));
        csScript.setSceneManager(solarSystemName);

    }
}
