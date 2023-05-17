using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuMoney : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _donatMoneyText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    private float _money;
    private float _donatMoney;
    private int _score;

    public float Money 
    { 
        get => _money; 
        set 
        {
            _money = value;
            _moneyText.text = _money.ToString();
        }
    }
    public float DonatMoney 
    { 
        get => _donatMoney;
        set
        {
            _donatMoney = value;
            _donatMoneyText.text = _donatMoney.ToString();
        }
    }

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            _scoreText.text = _score.ToString();
        }
    }
}
