using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopButtonsActions : MonoBehaviour {

    public Text diamondAmountText;
    public Text deuterAmountText;
    public Text antimatterAmountText;
    public Text terbAmountText;
    public Text moneyAmountText;


    private Economy _economy;

    
    private int _currentDiamondToSell = 0;
    private int _currentDeuterToSell = 0;
    private int _currentAntimatterToSell = 0;
    private int _currentTerbToSell = 0;

    private TurnOnOffScripts _scriptManager;

    private int _amountOfResource = 10;
	
    // Use this for initialization
	void Start () {
        _economy = GameObject.Find("_EconomicMechanism").GetComponent<Economy>();
        _scriptManager = GameObject.Find("_SceneManager").GetComponent<TurnOnOffScripts>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddDiamond()
    {
        _currentDiamondToSell = _currentDiamondToSell + _amountOfResource;
        if(_currentDiamondToSell > _economy.getDiamond())
        {
            _currentDiamondToSell = (int)_economy.getDiamond();
        }
        DisplayAllValues();
    }

    public void SubstractDiaomd()
    {
        _currentDiamondToSell = _currentDiamondToSell - _amountOfResource;
        if (_currentDiamondToSell < 0)
        {
            _currentDiamondToSell = 0;
        }
        DisplayAllValues();
    }

    public void AddDeuter()
    {
        _currentDeuterToSell = _currentDeuterToSell + _amountOfResource;
        if (_currentDeuterToSell > _economy.getDeuter())
        {
            _currentDeuterToSell= (int)_economy.getDeuter();
        }
        DisplayAllValues();
    }

    public void SubstractDeuter()
    {
        _currentDeuterToSell = _currentDeuterToSell - _amountOfResource;
        if (_currentDeuterToSell < 0)
        {
            _currentDeuterToSell = 0;
        }
        DisplayAllValues();
    }

    public void AddAntimatter()
    {
        _currentAntimatterToSell = _currentAntimatterToSell + _amountOfResource;
        if (_currentAntimatterToSell > _economy.getAntimatter())
        {
            _currentAntimatterToSell = (int)_economy.getAntimatter();
        }
        DisplayAllValues();
    }

    public void SubstractAntimatter()
    {
        _currentAntimatterToSell = _currentAntimatterToSell - _amountOfResource;
        if (_currentAntimatterToSell < 0)
        {
            _currentAntimatterToSell = 0;
        }
        DisplayAllValues();
    }

    public void AddTerb()
    {
        _currentTerbToSell = _currentTerbToSell + _amountOfResource;
        if (_currentTerbToSell > _economy.getTerb())
        {
            _currentTerbToSell = (int)_economy.getTerb();
        }
        DisplayAllValues();
    }

    public void SubstractTerb()
    {
        _currentTerbToSell = _currentTerbToSell - _amountOfResource;
        if (_currentTerbToSell < 0)
        {
            _currentTerbToSell = 0;
        }
        DisplayAllValues();
    }

    private int CalculateMoney()
    {
        int tempMoney = 0;
        tempMoney =  (_currentDiamondToSell * 2) + (_currentDeuterToSell * 4) + (_currentAntimatterToSell * 8) + (_currentTerbToSell * 16);
        return tempMoney;
    }

    private void ZeroAllValues()
    {
        _currentDiamondToSell = 0;
        _currentDeuterToSell = 0;
        _currentAntimatterToSell = 0;
        _currentTerbToSell = 0;
        DisplayAllValues();
        
    }

    private void DisplayAllValues()
    {
        diamondAmountText.text = _currentDiamondToSell.ToString();
        deuterAmountText.text = _currentDeuterToSell.ToString();
        antimatterAmountText.text = _currentAntimatterToSell.ToString();
        terbAmountText.text = _currentTerbToSell.ToString();
        moneyAmountText.text = CalculateMoney().ToString();
    }

    public void CloseShop()
    {
        ZeroAllValues();
        _scriptManager.turnOnOffShopInfo(false);
    }

    public void SellButtonAction()
    {
        _economy.addDiamond(_currentDiamondToSell * (-1));
        _economy.addDeuter(_currentDeuterToSell * (-1));
        _economy.addAntimatter(_currentAntimatterToSell * (-1));
        _economy.addTerb(_currentTerbToSell * (-1));
        _economy.addMoney(CalculateMoney());
        _economy.UpdateResources();
        ZeroAllValues();
    }

    public void SetAmountOfResource(int amount)
    {
        _amountOfResource = amount;
    }
}
