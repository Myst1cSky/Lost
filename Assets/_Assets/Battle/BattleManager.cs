using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetingComponent))]

public class BattleManager : MonoBehaviour, ITargetService
{
    List<BattleSite> mBattleSites;
    List<BattleCharacters> mBattleCharacters = new List<BattleCharacters>();

    Queue<BattleCharacters> mFirstRoundBattleCharacters = new Queue<BattleCharacters>();

    TargetingComponent mTargetingComponent;
    //int mRoundNumber = 1;
    //int mFirstTurnNextIndex = 0;
    
   void Awake()
   {
       mTargetingComponent = GetComponent<TargetingComponent>();
       mTargetingComponent.SetTargetService(this);
   }

   public void StartBattle(BattlePartyComponent playerParty, BattlePartyComponent enemyParty)
   {
        mBattleCharacters.Clear();
        if (mBattleSites == null)
        {
            mBattleSites = new List<BattleSite>();
            mBattleSites.AddRange(GameObject.FindObjectsByType<BattleSite>(FindObjectsSortMode.None));
        }
        Debug.Log($"Starting Battle between: {playerParty.gameObject.name} and {enemyParty.gameObject.name}");
        PrepParty(playerParty);
        PrepParty(enemyParty);
        StartCoroutine(StartTurns());
   }

    IEnumerator StartTurns()
    {
        //TODO: refactor to not hard code the delay
        yield return new WaitForSeconds(2);
        UpdateTurnOrder();
        mFirstRoundBattleCharacters = new Queue<BattleCharacters>(mBattleCharacters);
        ProcessFirstRound();
    }

    private void ProcessFirstRound()
    {
        if (mFirstRoundBattleCharacters.TryDequeue(out BattleCharacters nextBattleCharacter))
        {
            if (mBattleCharacters.Contains(nextBattleCharacter))
            {
                nextBattleCharacter.TakeTurn();
            }
            else
            {
                ProcessFirstRound();
            }
                return;
        }

        foreach (BattleCharacters battlecharacter in mBattleCharacters)
        {
            battlecharacter.OnTurnFinished -= ProcessFirstRound;
            battlecharacter.OnTurnFinished += NextTurn;
        }

        NextTurn();
    }

    void NextTurn()
    {
        //if (mRoundNumber == 1)
        //{
            //BattleCharacters nextInFirstTurn = mBattleCharacters[mFirstTurnNextIndex];
            //nextInFirstTurn.TakeTurn();
            //mFirstTurnNextIndex++;
            //if (mFirstTurnNextIndex >= mBattleCharacters.Count)
            //{
                //mRoundNumber = 2;
            //}
            //return;
        //}

        UpdateTurnOrder();
        

        float advanceTime = mBattleCharacters[0].CooldownTimeRemaining;
        foreach (BattleCharacters battleCharacters in mBattleCharacters)
        {
            battleCharacters.AdvanceCooldown(advanceTime);
        }

        BattleCharacters nextInTurn = mBattleCharacters[0];
        nextInTurn.TakeTurn();

        mBattleCharacters.Remove(nextInTurn);
        mBattleCharacters.Add(nextInTurn);


    }

    private void UpdateTurnOrder()
    {
        mBattleCharacters = mBattleCharacters.OrderBy(
            (battleCharacter) => { return battleCharacter.CooldownTimeRemaining; }).
            ThenBy((battleCharacter)=> { return 1/battleCharacter.Speed; }).
            ToList();
    }

    private void PrepParty(BattlePartyComponent party)
    {
        BattleSite partyBattleSite = mBattleSites.Find((battleSite) => { return !battleSite.IsPlayerSite; });
        if (party.gameObject.CompareTag("Player"))
        {
            partyBattleSite = mBattleSites.Find((battleSite) => { return battleSite.IsPlayerSite; });
        }

        int i = 0;
        foreach(BattleCharacters partyBattleCharacter in party.GetBattleCharacters())
        {
            partyBattleCharacter.transform.position = partyBattleSite.GetPositionForUnit(i);
            partyBattleCharacter.transform.rotation = partyBattleSite.transform.rotation;
            partyBattleCharacter.OnTurnFinished += ProcessFirstRound;
            mBattleCharacters.Add(partyBattleCharacter);
            i++;
        }

        party.FinishPrep();
    }

    public List<BattleCharacters> GetTargetsForTeam(int teamID, bool hostileTargets)
    {
        List<BattleCharacters> targets = new List<BattleCharacters>();
        foreach(BattleCharacters battleCharacter in mBattleCharacters)
        {
            if (battleCharacter.PartyID == teamID && !hostileTargets)
            {
                targets.Add(battleCharacter);
            }
            if (battleCharacter.PartyID != teamID && hostileTargets)
            {
                targets.Add(battleCharacter);
            }
        }
        return targets;
    }

    public TargetingComponent GetTargetingComponent()
    {
        return mTargetingComponent;
    }
}
