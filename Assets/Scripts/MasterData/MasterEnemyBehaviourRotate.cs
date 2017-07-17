using System;
using NeoC.Game.Model;
using UnityEngine;

[CreateAssetMenu]
public class MasterEnemyBehaviourRotate : MasterEnemyBehaviourBase
{
    public SquareModel rotateDelta;

    public override void Move(Action<SquareModel> move, Action<SquareModel> rotate)
    {
        rotate(rotateDelta);
    }
}
