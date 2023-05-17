using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices; //need to call js
using System.Xml.Linq;
using System.Runtime.Serialization.Json;
using UnityEngine.Networking.Types;

public class ShopScript : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _mainMenuCanvas;
    [SerializeField] private MainMenuUI _mainMenuUI;
    [SerializeField] private MenuMoney _money;
    [SerializeField] private List<ShopItem> _shopItems = new List<ShopItem>();
    [SerializeField] private List<ShopItemCharacter> _shopCharacterItems = new List<ShopItemCharacter>();
    private Action _rewardAction;
    private static bool _purchaseAvailable;
    private static List<string> _itemId = new List<string>();
    private static List<string> _itemPriceWithCurrency = new List<string>();
    private static List<string> _alreadyBought = new List<string>();
    public enum YandexPurchaseItemId //id add in ya games console shop itemId
    {
        removeAds,
        buyPack_1,
        buyPack_2,
        buyPack_3,
    }

    [DllImport("__Internal")]
    private static extern void Purchase(string id); //call js from plugin UnityScriptToJS.jslib
    public void ShowCanvas()
    {
        _mainMenuCanvas.SetActive(false);
        _canvas.SetActive(true);
    }
    public void HideCanvas()
    {
        _mainMenuCanvas.SetActive(true);
        _canvas.SetActive(false);
    }
    public void SetPurchaseAvailable()
    {
        _purchaseAvailable = true;
    }
    public void SetPurchaseUnavailable()
    {
        _purchaseAvailable = false;
    }
    public void SetItemsJsonDataFromYandex(string jsonData)
    {

        _itemId.Clear();
        _itemPriceWithCurrency.Clear();
        _alreadyBought.Clear();
        ProductsData data = JsonUtility.FromJson<ProductsData>(jsonData);
        for(int i = 0; i < data.productId.Length; i++)
        {
            _itemId.Add(data.productId[i]);
            _itemPriceWithCurrency.Add(data.productPrice[i]);
        }
        for(int i = 0; i < data.alreadyBought.Length; i++)
        {
            _alreadyBought.Add(data.alreadyBought[i]);
        }
        SetItems();
    }
    public void SetItems()
    {
        foreach (ShopItem item in _shopItems)
        {
            item.SetAvailable(false, "null");
            if (_purchaseAvailable && CheckForNotAlreadyBought(item.ItemId.ToString()))
            {
                for (int i = 0; i < _itemId.Count; i++)
                {
                    if (item.ItemId.ToString() == _itemId[i])
                    {
                        item.SetAvailable(true, _itemPriceWithCurrency[i]);
                        break;
                    }
                }
            }
        }
        foreach (ShopItemCharacter item in _shopCharacterItems)
        {
            item.SetAvailable();
        }
    }
    private bool CheckForNotAlreadyBought(string id)
    {
        for(int i = 0; i < _alreadyBought.Count; i++)
        {
            if(id == _alreadyBought[i])
            {
                return false;
            }
        }
        return true;
    }
    public void PurchaseSucces(string id) //result from js
    {
        _rewardAction?.Invoke();
        _rewardAction = null;
        FindObjectOfType<SaveGame>().SaveProgress();
    }
    public void TryBuy(YandexPurchaseItemId item, Action rewardAction)
    {
        _rewardAction = rewardAction;
        if (TestMode.Value == false)
        {
            Purchase(item.ToString());
        }
        else
        {
            PurchaseSucces(null);
        }
    }
}
[Serializable]
public class ProductsData
{
    public string[] productId;
    public string[] productTitle;
    public string[] productDescription;
    public string[] productImageURI;
    public string[] productPrice; //стоимость товара в формате <цена> <код валюты>. Например, «25 YAN».
    public string[] productPriceValue; //стоимость товара в формате <цена>. Например, «25».
    public string[] productPriceCurrencyCode; //код валюты(«YAN»).
    public string[] alreadyBought;
}