using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/BasicAttack")]
public class BasicAttack : Ability
{
    public override void ActivateAbility()
    {
        base.ActivateAbility();
        int partyId = OwningAbilityComponent.GetPartyID();
        List<BattleCharacters> targets = GameMode.MainGameMode.mBattleManager.GetTargetsForTeam(partyId, true);
        foreach (BattleCharacters battleCharacter in targets)
        {
            Debug.Log($"Found Target: {battleCharacter.gameObject.name}");
        }    

    }
}
