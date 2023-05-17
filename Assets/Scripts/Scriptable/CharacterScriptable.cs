using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character_", menuName = "ScriptableObjects/Character", order = 1)]
public class CharacterScriptable : ScriptableObject
{
    [ScriptableObjectId] public string itemId;
    [SerializeField] private string _itemNameRus;
    [SerializeField] private string _itemNameEng;
    [SerializeField] private GameObject _menuObjPrefub;
    [SerializeField] private GameObject _battleObjPrefub;
    [SerializeField] private float[] _damageByLevel;
    [SerializeField] private float[] _hpByLevel;
    [SerializeField] private float[] _upgradeCost; //one less count than dmg and hp

    public string ItemName
    {
        get
        {
            switch (Languages.CurrentLanguage)
            {
                case Languages.AllLanguages.Rus:
                    return _itemNameRus;
                case Languages.AllLanguages.Eng:
                    return _itemNameEng;
            }
            return _itemNameEng;
        }
    }

    public GameObject BattleObjPrefub { get => _battleObjPrefub; }
    public GameObject MenuObjPrefub { get => _menuObjPrefub; }
    public float[] DamageByLevel { get => _damageByLevel;  }
    public float[] HpByLevel { get => _hpByLevel;  }
    public float[] UpgradeCost { get => _upgradeCost;  }
}
