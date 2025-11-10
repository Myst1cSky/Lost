using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityComponent : MonoBehaviour
{
    [SerializeField] Ability[] mInitialAbilities;
    [SerializeField] Transform mTargetingFollowTransform;
    List<Ability> mAbilities = new List<Ability>();

    IViewClient mOwnerViewClient;
    public int GetPartyID()
    {
        return GetComponent<BattleCharacters>().PartyID;
    }

    void Start()
    {
        foreach (Ability initialAbility in mInitialAbilities)
        {
            GiveAbility(initialAbility);
        }
    }

    public void StartTargeting(bool hostile)
    {
        if (mOwnerViewClient is not  null)
        {
            mOwnerViewClient.PushViewTarget(mTargetingFollowTransform);
        }
        GameMode.MainGameMode.mBattleManager.GetTargetingComponent().StartTargeting(GetPartyID(), hostile);
    }

    private void GiveAbility(Ability abilityDefaultObject)
    {
        Ability newAbility = Instantiate(abilityDefaultObject);
        newAbility.Init(this);
        mAbilities.Add(newAbility);
    }

    internal IEnumerable<Ability> GetAbilities()
    {
        return mAbilities;
    }

    internal void SetViewClient(IViewClient viewClient)
    {
        mOwnerViewClient = viewClient;
    }
}
