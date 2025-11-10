using System;
using System.Collections.Generic;
using UnityEngine;

public class BattlePartyComponent : MonoBehaviour
{
    [SerializeField] BattleCharacters[] mBattleCharactersPrefabs;
    List<BattleCharacters> mBattleCharacters;

    IViewClient mOwnerViewClient;

    public event Action<BattleCharacters> onBattleCharacterTakeTurn;

    [field: SerializeField] public int PartyID { get; private set; } = 0;

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
            mOwnerViewClient.PushViewTarget(mBattleCharacters[0].transform);
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
                newBattleCharacter.Init(PartyID, mOwnerViewClient);
                newBattleCharacter.onTurnStarted += CharacterInTurn;
                mBattleCharacters.Add((newBattleCharacter));
            }
        }

        return mBattleCharacters;
    }

    private void CharacterInTurn(BattleCharacters character)
    {
        onBattleCharacterTakeTurn?.Invoke(character);
        if (mOwnerViewClient is not null && character)
        {
            mOwnerViewClient.PushViewTarget(character.transform);
        }
    }
}
