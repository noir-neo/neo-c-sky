using System;
using System.Collections.Generic;
using NeoC.Game.Model;
using UnityEngine;

[CreateAssetMenu]
public class MasterEnemyBehaviourMultiple : MasterEnemyBehaviourBase
{
    public List<MasterEnemyBehaviourBase> behaviours;

    public override void Move(Action<SquareModel> move, Action<SquareModel> rotate)
    {
        foreach (var behaviour in behaviours)
        {
            behaviour.Move(move, rotate);
        }
    }
}
