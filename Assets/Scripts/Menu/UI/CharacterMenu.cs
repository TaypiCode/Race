using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterMenu : MonoBehaviour
{
    [SerializeField] private MainMenuUI _mainMenuUI;
    [SerializeField] private MenuMoney _money;
    [SerializeField] private CharacterPreview _characterPreview;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _upgradeCostText;
    [SerializeField] private GameObject _upgradeCostTextCanvas;
    [SerializeField] private GameObject _maxLevelTextCanvas;
    [SerializeField] private GameObject _upgradeBtn;
    [SerializeField] private GameObject _selectCharacterInputsCanvas;
    private int _currentIndex;
    private int _characterLevel;
    private float _upgradeCost;
    private CharacterScriptable _selectedCharacter;
    public void ShowCanvas()
    {
        _currentIndex = _mainMenuUI.GetOwnedCharacters().IndexOf(PlayerData.SelectedCharacter);
        SelectCharacter(_currentIndex);
        _canvas.SetActive(true);
        _menuCanvas.SetActive(false);
    }
    public void HideCanvas()
    {
        _canvas.SetActive(false);
        _menuCanvas.SetActive(true); 
    }
    public void SelectCharacter(int index)
    {
        _currentIndex = index;
        _selectedCharacter = _mainMenuUI.GetOwnedCharacters()[index];
        PlayerData.SelectedCharacter = _selectedCharacter;
        PlayerData.CharacterLevel = _mainMenuUI.GetCharacterLevel(_selectedCharacter);
        _characterPreview.ShowCharacter(index);
        _characterLevel = _mainMenuUI.GetCharacterLevel(_selectedCharacter);
        _levelText.text = _characterLevel.ToString();
        _hpText.text = _selectedCharacter.HpByLevel[_characterLevel - 1].ToString();
        _damageText.text = _selectedCharacter.DamageByLevel[_characterLevel - 1].ToString();
        CheckForUpgrade();
        CheckForSelectCharacter();
    }
    public void NextCharacter()
    {
        int newIndex = _currentIndex + 1;
        if (newIndex >= _mainMenuUI.GetOwnedCharacters().Count)
        {
            newIndex = 0;
        }
        SelectCharacter(newIndex);
    }
    public void PreviousCharacter()
    {
        int newIndex = _currentIndex - 1;
        if (newIndex < 0)
        {
            newIndex = _mainMenuUI.GetOwnedCharacters().Count - 1;
        }
        SelectCharacter(newIndex);
    }
    private void CheckForSelectCharacter()
    {
        _selectCharacterInputsCanvas.SetActive(_mainMenuUI.GetOwnedCharacters().Count > 1);
    }
    private void CheckForUpgrade()
    {
        _characterLevel = _mainMenuUI.GetCharacterLevel(_selectedCharacter);
        if (_characterLevel < _selectedCharacter.UpgradeCost.Length) 
        {
            _upgradeBtn.SetActive(true);
            _upgradeCostTextCanvas.SetActive(true);
            _maxLevelTextCanvas.SetActive(false);
            _upgradeCost = _selectedCharacter.UpgradeCost[_characterLevel - 1];
            _upgradeCostText.text = _upgradeCost.ToString();
        }
        else
        {
            _upgradeBtn.SetActive(false);
            _upgradeCostTextCanvas.SetActive(false);
            _maxLevelTextCanvas.SetActive(true);
        }
    }
    public void TryUpgrade()
    {
        if (_money.Money >= _upgradeCost)//succes
        {
            _money.Money -= _upgradeCost;
            _mainMenuUI.PlusLevel(_selectedCharacter);
            SelectCharacter(_currentIndex);
            FindObjectOfType<SaveGame>().SaveProgress();
        }
    }
}
