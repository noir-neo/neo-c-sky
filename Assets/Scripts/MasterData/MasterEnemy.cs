using System;
using System.Collections.Generic;
using System.Linq;
using NeoC.Game.Model;
using UnityEngine;

[CreateAssetMenu]
public class MasterEnemy : ScriptableObject
{
    public List<MasterEnemyBehaviourBase> behaviours;

    public void Move(int index, Action<SquareModel> move, Action<SquareModel> rotate)
    {
        var behaviour = behaviours.ElementAtOrDefault(index % behaviours.Count);
        if (behaviour == null)
        {
            return;
        }
        behaviour.Move(move, rotate);
    }
}
