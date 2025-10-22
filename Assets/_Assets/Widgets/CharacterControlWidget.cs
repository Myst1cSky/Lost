using System;
using TMPro;
using UnityEngine;

public class CharacterControlWidget : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mCharacterNameText;
    internal void SetBattleCharacter(BattleCharacters battleCharacter)
    {
        Debug.Log($"Setting Battle Character name to: {battleCharacter.gameObject.name}");
        mCharacterNameText.SetText(battleCharacter.Name);
    }
}
