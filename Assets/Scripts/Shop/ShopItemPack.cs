using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemPack : ShopItem
{
    [SerializeField] private float _donatMoneyCount;

    public override void Reward()
    {
        FindObjectOfType<MenuMoney>().DonatMoney += _donatMoneyCount;
    }
}
