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

    public int PartyID { get; private set; }

    public void Init(int partyID, IViewClient viewClient)
    {
        PartyID = partyID;
        if (mAbilityComponent == null)
        {
            mAbilityComponent = GetComponent<AbilityComponent>();
            mAbilityComponent.SetViewClient(viewClient);
        }
        if (mAbilityComponent != null)
        {
            mAbilityComponent.SetViewClient(viewClient);
        }
    }

    public AbilityComponent GetAbilityComponent()
    {
        return mAbilityComponent;
    }

    void Awake()
    {
        CooldownTimeRemaining = CooldownDuration;
        mTurnIndicator.SetActive(false);
        mAbilityComponent = GetComponent<AbilityComponent>();
    }

    public void SetHighLighted(bool highted)
    {
        mTurnIndicator.SetActive(highted);
    }

    public void TakeTurn()
    {
        SetHighLighted(true);
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
