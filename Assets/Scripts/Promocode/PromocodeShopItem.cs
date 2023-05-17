using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PromocodeShopItem : MonoBehaviour
{
    [SerializeField] private CharacterScriptable _character;
    [SerializeField] private PromocodeScriptable _promocode;
    [SerializeField] private GameObject _object;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private MainMenuUI _menu;

    public PromocodeScriptable Promocode { get => _promocode;  }

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
            if (id == _character.itemId)
            {
                _object.SetActive(false);
                return;
            }
        }
        _nameText.text = _character.ItemName;
        _object.SetActive(true);
    }
    public void ShowPromocodeCanvas()
    {
        FindObjectOfType<Promocode>().ShowCanvas(Reward);
    }
    public void Reward()
    {
        _menu.AddOwnCharacter(_character, 1);
        SetAvailable(false);
    }
}
