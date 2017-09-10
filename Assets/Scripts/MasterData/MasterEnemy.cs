using System;
using System.Collections.Generic;
using System.Linq;
using NeoC.Game;
using NeoC.Game.Model;
using UnityEngine;

[CreateAssetMenu]
public class MasterEnemy : ScriptableObject
{
    public List<MasterEnemyBehaviourBase> behaviours;
    public MasterOccupiedRange occupiedRange;
    public PieceMover piecePrefab;

    public void Move(int index, Action<SquareModel> move, Action<SquareModel> rotate)
    {
        if (behaviours.Count < 1) return;

        var behaviour = behaviours.ElementAtOrDefault(index % behaviours.Count);
        if (behaviour == null)
        {
            return;
        }
        behaviour.Move(move, rotate);
    }
}
