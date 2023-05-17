using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class PlayerData
{
    private static int _score;
    private static CharacterScriptable _selectedCharacter;
    private static BattleDataScriptable _battleScriptable;
    private static float _addMoneyForBattle;
    private static float _addDonatMoneyForBattle;
    private static int _addScoreBattle;
    private static int _characterLevel;
    private static int _battleWin;
    public static int Score { get => _score; set => _score = value; }
    public static CharacterScriptable SelectedCharacter { get => _selectedCharacter; set => _selectedCharacter = value; }
    public static BattleDataScriptable BattleScriptable { get => _battleScriptable; set => _battleScriptable = value; }
    public static float AddMoneyForBattle { get => _addMoneyForBattle; set => _addMoneyForBattle = value; }
    public static float AddDonatMoneyForBattle { get => _addDonatMoneyForBattle; set => _addDonatMoneyForBattle = value; }
    public static int CharacterLevel { get => _characterLevel; set => _characterLevel = value; }
    public static int BattleWin { get => _battleWin; set => _battleWin = value; }
    public static int AddScoreBattle { get => _addScoreBattle; set => _addScoreBattle = value; }

    public static bool IsPointerOverLayer(string layer)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        foreach (RaycastResult raysastResult in raysastResults)
        {
            if (raysastResult.gameObject.layer == LayerMask.NameToLayer(layer))
            {
                return true;
            }
        }
        return false;
    }
}
