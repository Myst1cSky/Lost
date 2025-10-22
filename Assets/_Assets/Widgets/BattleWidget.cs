using UnityEngine;
using UnityEngine.UI;

public class BattleWidget : MonoBehaviour
{
    [SerializeField] CharacterControlWidget mCharacterControllWidget;
    [SerializeField] LayoutGroup mAbilityListLayoutGroup;
    [SerializeField] AbilityWidget mAbilityWidgetPrefab;
    
    public void SetCharacterControlTarget(BattleCharacters battleCharacter)
    {
        foreach(Transform existingEntries in mAbilityListLayoutGroup.transform)
        {
            Destroy(existingEntries.gameObject);
        }

        mCharacterControllWidget.gameObject.SetActive(true);
        mCharacterControllWidget.SetBattleCharacter(battleCharacter);
        AbilityComponent abilityComponent = battleCharacter.GetAbilityComponent();
        if (abilityComponent)
        {
            foreach(Ability ability in abilityComponent.GetAbilities())
            {
                AddAbilityToAbilityList(ability);
            }
        }
    }

    private void AddAbilityToAbilityList(Ability ability)
    {
        AbilityWidget newAbilityWidget = Instantiate(mAbilityWidgetPrefab, mAbilityListLayoutGroup.transform);
        newAbilityWidget.SetAbility(ability);
    }
}
