using System;
using NeoC.Game.Model;
using UnityEngine;

[CreateAssetMenu]
public class MasterEnemyBehaviourMove : MasterEnemyBehaviourBase
{
    public SquareModel moveDelta;

    public override void Move(Action<SquareModel> move, Action<SquareModel> rotate)
    {
        move(moveDelta);
    }
}
