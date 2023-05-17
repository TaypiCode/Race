using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    [SerializeField] private MainMenuUI _mainMenuUI;
    [SerializeField] private MenuMoney _money; 
    [SerializeField] private Languages _language;
    private Save _save;
    private static bool _firstLoad = true;
    private static bool _isMobile;

    public static bool IsMobile { get => _isMobile;  }

    [DllImport("__Internal")]
    private static extern void FirstLoadInSession(); //call js from plugin UnityScriptToJS.jslib

    private void Awake()
    {
        if (PlayerPrefs.HasKey("SV"))
        {
            _save = new Save();
            _save = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("SV"));
        }
        if (!_firstLoad)
        {
            LoadData();
        }

    }
    private void Start()
    {
        if (_firstLoad)
        {
            if (TestMode.Value == true)
            {
                LoadData();
            }
            else
            {
                FirstLoadInSession();
            }
        }
    }
    public void LoadFromYandex(string data)
    {
        _save = new Save();
        _save = JsonUtility.FromJson<Save>(data);
        FindObjectOfType<SaveGame>().SaveJson(_save);
        LoadData();
    }

    public void LoadData()
    {
        if (_save != null)
        {
            if (_save.currentLanguage != null)
            {
                _language.SetLanguage(_save.currentLanguage, false);
            }
            else
            {
                _language.ShowChooseLanguage();
            }
            if (_save.activatedPromocodes != null)
            {
                FindObjectOfType<Promocode>().FillActivatedPromocodes(_save.activatedPromocodes);
            }
            _mainMenuUI.LoadOwnedCharacters(_save.ownCharactersId, _save.ownCharactersLevel);
            _money.Money = _save.money;
            _money.DonatMoney = _save.donatMoney;
            _money.Score = _save.score;
        }
        else
        {
            //no save
            _mainMenuUI.LoadOwnedCharacters(null, null);
            _money.Money= 0;
            _money.DonatMoney= 0; 
            _money.Score= 0; 
        }
        _money.Money += PlayerData.AddMoneyForBattle;
        _money.DonatMoney += PlayerData.AddDonatMoneyForBattle;
        _money.Score += PlayerData.AddScoreBattle;
        PlayerData.AddMoneyForBattle = 0;
        PlayerData.AddDonatMoneyForBattle = 0;
        PlayerData.AddScoreBattle = 0;
        FindObjectOfType<ShopScript>().SetItems();
        FindObjectOfType<Promocode>().SetPromocodeShopItems();
        FindObjectOfType<SaveGame>().SaveProgress();
        if(PlayerData.BattleWin != 0 && PlayerData.BattleWin >= 2)
        {
            RateUsScript.ShowRateUs();
            PlayerData.BattleWin = 0;
        }
        if (!_firstLoad)
        {
            FindObjectOfType<AdsScript>().ShowNonRewardAd();
        }
        _firstLoad = false;
    }
    public void SetIsMobile()
    {
        _isMobile = true;
    }
}
