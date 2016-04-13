using UnityEngine;
using System.Collections;

public class TurnOnOffScripts : MonoBehaviour {

    private GameObject _solarSystem;
    private GameObject _planetEarth;
    private GameObject _objectPanel;
    private GameObject _rocketPanel;
    private GameObject _shopPanel;
    private GameObject _rocketMoreInfoPanel;
    private GameObject _errorPanel;
    private GameObject _planetaryBacground;
    private ZoomAndMove _zoomPossibility;
    private Camera _camera;

    // Use this for initialization
    void Start () {
        _planetEarth = GameObject.Find("PlanetEarth");
        _solarSystem = GameObject.Find("SolarSystem");
        _objectPanel = GameObject.Find("Canvas/PanelInfo");
        _rocketPanel = GameObject.Find("Canvas/PanelRocketInfo");
        _shopPanel = GameObject.Find("Canvas/PanelShopInfo");
        _rocketMoreInfoPanel = GameObject.Find("Canvas/PanelRocketMoreInfo");
        _errorPanel = GameObject.Find("Canvas/PanelError");
        _zoomPossibility = GameObject.Find("MainCamera").GetComponent<ZoomAndMove>();
        _planetaryBacground = GameObject.Find("MainCamera/PlanetaryBackground");
        _camera = Camera.main;
        turnOnOffErrorPanel(false);
        turnOnOffRocketMorePanelInfo(false);
        turnOnOffShopInfo(false);
        turnOnOffRocketInfo(false);
        turnOnOffPlanetEarth(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    
    public void CameraMovement(bool condition)
    {
        _zoomPossibility.enabled = condition;
       /* if(condition == false)
        {
            Vector3 standartPos = new Vector3(0f, 0f, -10f);
            _camera.transform.position = standartPos;
            _camera.orthographicSize = 5;
        }
        */
    }

    public void turnOnOffPlanetaryBackground(bool condition)
    {
        _planetaryBacground.SetActive(condition);
    }

    public void turnOnOffErrorPanel(bool condition)
    {
        _errorPanel.SetActive(condition);
    }

    public void turnOnOffRocketMorePanelInfo(bool condition)
    {
        _rocketMoreInfoPanel.SetActive(condition);
    }

    public void turnOnOffShopInfo(bool condition)
    {
        _shopPanel.SetActive(condition);
    }

    public void turnOnOffRocketInfo(bool condition)
    {
        _rocketPanel.SetActive(condition);
    }

    public void turnOnOffPlanetEarth(bool condition)
    {
        _planetEarth.SetActive(condition);
    }

    public void turnOnOffSolarSystem(bool conditon)
    {
        _solarSystem.SetActive(conditon);
    }

    public void turnOnOffPanel(bool condition)
    {
        _objectPanel.SetActive(condition);
    }

    public bool getSolarSystemStatus()
    {
       if(_solarSystem.active == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
