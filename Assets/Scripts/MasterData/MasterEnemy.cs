using System;
using System.Collections.Generic;
using System.Linq;
using NeoC.Game.Model;
using UnityEngine;

[CreateAssetMenu]
public class MasterEnemy : ScriptableObject
{
    public GameObject piecePrefab;
    public List<MasterEnemyBehaviourBase> behaviours;
    public MasterOccupiedRange occupiedRange;

    public GameObject InstantiatePiece(Vector3 position, Quaternion rotation)
    {
        return Instantiate(piecePrefab, position, rotation);
    }

    public GameObject InstantiatePiece()
    {
        return Instantiate(piecePrefab, Vector3.zero, Quaternion.identity);
    }

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
