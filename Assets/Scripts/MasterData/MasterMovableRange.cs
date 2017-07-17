using System.Collections.Generic;
using System.Linq;
using NeoC.Game.Model;
using UnityEngine;

[CreateAssetMenu]
public class MasterMovableRange : ScriptableObject
{
    public List<SquareModel> movableRange;

    public IEnumerable<SquareModel> MovableSquares(SquareModel offset)
    {
        return movableRange.Select(s => s + offset);
    }

}
