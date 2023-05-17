using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPreview : MonoBehaviour
{
    [SerializeField] private Transform _spawnTransform;
    private List<GameObject> _characters = new List<GameObject>();
    private GameObject _showedCharacter;
    public void SpawnCharacter(GameObject character)
    {
        GameObject obj = Instantiate(character, _spawnTransform);
        _characters.Add(obj);
        obj.SetActive(false);
    }
    public void ShowCharacter(int id)
    {
        _showedCharacter?.SetActive(false);
        _characters[id].SetActive(true);
        _showedCharacter = _characters[id];
    }
}
