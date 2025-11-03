using System.Collections.Generic;
using UnityEngine;

public interface ITargetService
{
    public List<BattleCharacters> GetTargetsForTeam(int teamID, bool hostileTargets);

    public TargetingComponent GetTargetingComponent();
}
