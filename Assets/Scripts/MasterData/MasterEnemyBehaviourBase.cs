using System;
using NeoC.Game.Model;
using UnityEngine;

[CreateAssetMenu]
public abstract class MasterEnemyBehaviourBase : ScriptableObject
{
    public abstract void Move(Action<SquareModel> move, Action<SquareModel> rotate);
}
