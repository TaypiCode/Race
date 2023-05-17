using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseLevel : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _mainMenuCanvas;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Image _previewImage;
    [SerializeField] private List<BattleDataScriptable> _battles = new List<BattleDataScriptable>();
    private int _currentIndex;
    public void ShowCanvas()
    {
        if(PlayerData.BattleScriptable == null)
        {
            PlayerData.BattleScriptable = _battles[0];
        }
        _currentIndex = _battles.IndexOf(PlayerData.BattleScriptable);
        SetBattle(_currentIndex);
        _canvas.SetActive(true);
        _mainMenuCanvas.SetActive(false);
    }
    public void HideCanvas()
    {
        _canvas.SetActive(false);
        _mainMenuCanvas.SetActive(true);
    }
    private void SetBattle(int index)
    {
        PlayerData.BattleScriptable = _battles[index];
        _currentIndex = index;
        _previewImage.sprite = _battles[index].ImgPreview;
        _nameText.text = _battles[index].GetName;
        int enemies = _battles[index].TotalEnemiesCount;
        string[] text = new string[3];
        string newStr = "\n";
        switch (Languages.CurrentLanguage)
        {
            case Languages.AllLanguages.Rus:
                text[0] = "Врагов: ";
                text[1] = "$ за зомби: ";
                text[2] = "D за победу: ";
                break;
            case Languages.AllLanguages.Eng:
                text[0] = "Enemies: ";
                text[1] = "$ per zombie: ";
                text[2] = "D for win: ";
                break;
        }
        _descriptionText.text = text[0] + enemies;
        _descriptionText.text += newStr + text[1] + _battles[index].MoneyForEnemy;
        _descriptionText.text += newStr + text[2] + _battles[index].DonatMoneyForWin;
    }
    public void NextBattle()
    {
        int newIndex = _currentIndex + 1;
        if(newIndex >= _battles.Count) 
        {
            newIndex = 0;
        }
        SetBattle(newIndex);
    }
    public void PreviousBattle()
    {
        int newIndex = _currentIndex - 1;
        if (newIndex < 0)
        {
            newIndex = _battles.Count - 1;
        }
        SetBattle(newIndex);
    }
    public void PlayBattle()
    {
        FindObjectOfType<SaveGame>().SaveProgress();
        SceneManager.LoadScene(PlayerData.BattleScriptable.SceneId);
    }
}
