using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopItemCharacter : MonoBehaviour
{
    [SerializeField] private CharacterScriptable _character;
    [SerializeField] private float _price;
    [SerializeField] private Currency _currency;
    [SerializeField] private GameObject _object;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private MenuMoney _money;
    [SerializeField] private MainMenuUI _menu;
    public enum Currency
    {
        Money,
        DonatMoney
    }
    public void SetAvailable(bool val = true)
    {
        if (val == false)
        {
            _object.SetActive(val);
            return;
        }
        string[] owned = _menu.GetOwnedCharactersId();
        foreach (string id in owned) 
        {
            if(id == _character.itemId)
            {
                _object.SetActive(false);
                return;
            }
        }
        string currencyStr = "";
        switch (_currency)
        {
            case Currency.Money:
                currencyStr = " $";
                break;
            case Currency.DonatMoney:
                currencyStr = " D";
                break;
        }
        switch (Languages.CurrentLanguage)
        {
            case Languages.AllLanguages.Rus:
                _priceText.text = "Цена: " + _price + currencyStr;
                break;
            case Languages.AllLanguages.Eng:
                _priceText.text = "Price: " + _price + currencyStr;
                break;
        }
        _nameText.text = _character.ItemName;
        _object.SetActive(true);
    }
    public void TryBuy()
    {
        switch (_currency)
        {
            case Currency.Money:
                if (_money.Money >= _price)
                {
                    _money.Money -= _price;
                    AddCharcter();
                }
                break;
            case Currency.DonatMoney:
                if (_money.DonatMoney >= _price)
                {
                    _money.DonatMoney -= _price;
                    AddCharcter();
                }
                break;
        }

    }
    private void AddCharcter()
    {
        _menu.AddOwnCharacter(_character, 1);
        SetAvailable(false);
    }
}
