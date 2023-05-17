using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Battle_", menuName = "ScriptableObjects/BattleDataScriptable", order = 1)]
public class BattleDataScriptable : ScriptableObject
{
    [ScriptableObjectId] public string itemId;
    [SerializeField] private int _sceneId;
    [SerializeField] private string _nameRus;
    [SerializeField] private string _nameEng;
    [SerializeField] private Sprite _imgPreview;
    [SerializeField] private int _totalEnemiesCount;
    [SerializeField] private int _maxEnemiesOnScene;
    [SerializeField] private int _maxAmmoBoxOnScene; //less or equal with spawn pos
    [SerializeField] private int _maxHealthBoxOnScene; //less or equal with spawn pos
    [SerializeField] private float _enemySpawnDelay;
    [SerializeField] private float _moneyForEnemy;
    [SerializeField] private float _donatMoneyForWin;
    [SerializeField] private CharacterScriptable _enemyScriptable;
    [SerializeField] private CharacterScriptable _enemyBossScriptable;
    
    public string GetName
    {
        get
        {
            switch (Languages.CurrentLanguage)
            {
                case Languages.AllLanguages.Rus:
                    return _nameRus;
                case Languages.AllLanguages.Eng:
                    return _nameEng;
            }
            return _nameEng;
        }
    }
    public int TotalEnemiesCount { get => _totalEnemiesCount; }
    public int MaxEnemiesOnScene { get => _maxEnemiesOnScene;  }
    public int MaxAmmoBoxOnScene { get => _maxAmmoBoxOnScene;  }
    public int MaxHealthBoxOnScene { get => _maxHealthBoxOnScene;  }
    public float EnemySpawnDelay { get => _enemySpawnDelay;  }
    public CharacterScriptable EnemyScriptable { get => _enemyScriptable;  }
    public CharacterScriptable EnemyBossScriptable { get => _enemyBossScriptable;  }
    public Sprite ImgPreview { get => _imgPreview;  }
    public int SceneId { get => _sceneId;}
    public float MoneyForEnemy { get => _moneyForEnemy;}
    public float DonatMoneyForWin { get => _donatMoneyForWin;}
}
