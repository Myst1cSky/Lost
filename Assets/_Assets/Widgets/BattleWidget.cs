using UnityEngine;

public class BattleWidget : MonoBehaviour
{
    [SerializeField] CharacterControlWidget mCharacterControllWidget;
    
    public void SetCharacterControlTarget(BattleCharacters battleCharacter)
    {
        mCharacterControllWidget.gameObject.SetActive(true);
        mCharacterControllWidget.SetBattleCharacter(battleCharacter);
    }
}
