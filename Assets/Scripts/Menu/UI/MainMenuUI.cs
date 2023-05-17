using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private AllCharacters _allCharacters;
    [SerializeField] private CharacterScriptable _startedCharacter;
    [SerializeField] private CharacterPreview _characterPreview;
    private List<CharacterScriptable> _ownCharacters= new List<CharacterScriptable>();
    private List<int> _ownCharactersLevel= new List<int>();
    public void LoadOwnedCharacters(string[] charactersId, int[] charactersLevel)
    {
        if (charactersId == null || charactersLevel == null || charactersId.Length == 0 || charactersLevel.Length == 0)
        {
            charactersId= new string[1] { _startedCharacter.itemId };
            charactersLevel = new int[1] { 1 };
        }
        for(int i = 0; i < charactersId.Length; i++)
        {
            for(int j = 0; j < _allCharacters.Characters.Length; j++)
            {
                if (charactersId[i] == _allCharacters.Characters[j].itemId)
                {
                    AddOwnCharacter(_allCharacters.Characters[j], charactersLevel[i]);
                    break;
                }
            }
        }
        int selectedIndex = 0;
        if (PlayerData.SelectedCharacter != null)
        {
            try
            {
                selectedIndex = _ownCharacters.IndexOf(PlayerData.SelectedCharacter);
            }
            catch { }
        }
        FindObjectOfType<CharacterMenu>().SelectCharacter(selectedIndex);
    }
    public void AddOwnCharacter(CharacterScriptable character, int level)
    {
        _characterPreview.SpawnCharacter(character.MenuObjPrefub);
        _ownCharacters.Add(character);
        _ownCharactersLevel.Add(level);
    }
    public void PlusLevel(CharacterScriptable character)
    {
        for (int i = 0; i < _ownCharacters.Count; i++)
        {
            if (_ownCharacters[i] == character)
            {
                if (character.HpByLevel.Length > _ownCharactersLevel[i])
                {
                    _ownCharactersLevel[i]++;
                }
            }
        }
    }
    public string[] GetOwnedCharactersId()
    {
        List<string> ids = new List<string>();
        for(int i = 0; i < _ownCharacters.Count; i++)
        {
            ids.Add(_ownCharacters[i].itemId);
        }
        return ids.ToArray();
    }
    public int[] GetOwnedCharactersLevel()
    {
        return _ownCharactersLevel.ToArray();
    }
    public List<CharacterScriptable> GetOwnedCharacters()
    {
        return _ownCharacters;
    }
    public int GetCharacterLevel(CharacterScriptable character)
    {
        for(int i = 0; i < _ownCharacters.Count; i++)
        {
            if (_ownCharacters[i] == character)
            {
                return _ownCharactersLevel[i];
            }
        }
        return -1;
    }
}
