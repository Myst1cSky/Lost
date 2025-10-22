using System;
using UnityEngine;

[RequireComponent(typeof(AbilityComponent))]
public class BattleCharacters : MonoBehaviour
{
    [field: SerializeField] public float Speed { get; private set; } = 1;
    [field: SerializeField] public string Name { get; private set; } = "BattleCharacter";
    [SerializeField] GameObject mTurnIndicator;

    public float CooldownDuration => 1f / Speed;
    public float CooldownTimeRemaining { get; private set; }

    public event Action OnTurnFinished;

    public event Action<BattleCharacters> onTurnStarted;

    AbilityComponent mAbilityComponent;

    public AbilityComponent GetAbilityComponent()
    {
        return mAbilityComponent;
    }

    void Awake()
    {
        CooldownTimeRemaining = CooldownDuration;
        mTurnIndicator.SetActive(false);
    }

    public void TakeTurn()
    {
        Invoke("FinishTurn", 1);
        mTurnIndicator.SetActive(true);
        onTurnStarted?.Invoke(this);
        CooldownTimeRemaining = CooldownDuration;
    }

    public void FinishTurn()
    {
        mTurnIndicator.SetActive(false);
        OnTurnFinished?.Invoke();
    }

    internal void AdvanceCooldown(float advanceTime)
    {
        CooldownTimeRemaining -= advanceTime;
    }
}
