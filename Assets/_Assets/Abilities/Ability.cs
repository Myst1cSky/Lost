using System;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    [field: SerializeField] public string AbilityName { get; private set; }
    public AbilityComponent OwningAbilityComponent { get; private set; }
   internal void Init(AbilityComponent newAbility)
   {
        OwningAbilityComponent = newAbility;
   }

    public virtual void ActivateAbility()
    {
        Debug.Log($"Activating ability");
    }
}
