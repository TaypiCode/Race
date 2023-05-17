using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCharacters : MonoBehaviour
{
    [SerializeField] private CharacterScriptable[] _characters;
    public CharacterScriptable[] Characters { get => _characters; }
}
