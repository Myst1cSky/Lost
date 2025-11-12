using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/BasicAttack")]
public class BasicAttack : Ability
{
    BattleCharacters mTarget;
    [SerializeField] float mDamageAmt = 20f;
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
        mTarget = characters;
        OwningAbilityComponent.onTargetPicked -= TargetPicked;
        OwningAbilityComponent.onTargetCancelled -= TargetCancelled;

        Debug.Log($"Attacking: {characters.gameObject.name}");

        OwningAbilityComponent.MoveToTarget(characters.transform.position);

        OwningAbilityComponent.onMoveToTargetFinished -= MovedToTarget;
        OwningAbilityComponent.onMoveToTargetFinished += MovedToTarget;
    }

    private void MovedToTarget()
    {
        OwningAbilityComponent.onMoveToTargetFinished -= MovedToTarget;
        OwningAbilityComponent.GetComponent<Animator>().SetTrigger("Attack");
        OwningAbilityComponent.onGameplayEventReceived += HandleGameplayEvent;
    }

    private void HandleGameplayEvent(string eventTag)
    {
        if (eventTag == "ApplyDamage")
        {
            mTarget.TakeDamage(mDamageAmt);
            return;
        }

        if (eventTag == "AttackFinished")
        {
            OwningAbilityComponent.MoveBackToPartySpot();
        }
    }
}
