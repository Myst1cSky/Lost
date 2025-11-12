using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/BasicAttack")]
public class BasicAttack : Ability
{
    public override void ActivateAbility()
    {
        base.ActivateAbility();
        OwningAbilityComponent.StartTargeting(true);

        OwningAbilityComponent.onTargetPicked -= TargetPicked;
        OwningAbilityComponent.onTargetCancelled -= TargetCancelled;

        OwningAbilityComponent.onTargetPicked += TargetPicked;
        OwningAbilityComponent.onTargetCancelled += TargetCancelled;
    }

    private void TargetCancelled()
    {
        OwningAbilityComponent.onTargetPicked -= TargetPicked;
        OwningAbilityComponent.onTargetCancelled -= TargetCancelled;
        EndAbility();
    }

    private void TargetPicked(BattleCharacters characters)
    {
        OwningAbilityComponent.onTargetPicked -= TargetPicked;
        OwningAbilityComponent.onTargetCancelled -= TargetCancelled;

        Debug.Log($"attacking: {characters.gameObject.name}");

        OwningAbilityComponent.MoveToTarget(characters.transform.position);
    }
}
