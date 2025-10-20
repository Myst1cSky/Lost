using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BattlePartyComponent : MonoBehaviour
{
    [SerializeField] BattleCharacters[] mBattleCharactersPrefabs;
    List<BattleCharacters> mBattleCharacters;

    IViewClient mOwnerViewClient;

    void Awake()
    {
        mOwnerViewClient = GetComponent<IViewClient>();
    }

    public void FinishPrep()
    {
       
    }

    public void UpdateView()
    {
        if (mOwnerViewClient is not null)
        {
            mOwnerViewClient.SetViewTarget(mBattleCharacters[0].transform);
            mOwnerViewClient.ResetViewAngle();
        }
    }    

    public List<BattleCharacters> GetBattleCharacters()
    {
        if (mBattleCharacters == null)
        {
            mBattleCharacters = new List<BattleCharacters>();
            foreach (BattleCharacters battleCharacter in mBattleCharactersPrefabs)
            {
                BattleCharacters newBattleCharacter = Instantiate(battleCharacter);
                newBattleCharacter.onTurnStarted += ChangeViewTo;
                mBattleCharacters.Add((newBattleCharacter));
            }
        }

        return mBattleCharacters;
    }

    private void ChangeViewTo(BattleCharacters character)
    {
        if (mOwnerViewClient is not null && character)
        {
            mOwnerViewClient.SetViewTarget(character.transform);
        }
    }
}
