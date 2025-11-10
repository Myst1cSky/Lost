using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/BasicAttack")]
public class BasicAttack : Ability
{
    public override void ActivateAbility()
    {
        base.ActivateAbility();
        int partyId = OwningAbilityComponent.GetPartyID();
        GameMode.MainGameMode.mBattleManager.GetTargetingComponent().StartTargetting(partyId, true);

    }
}
