using System.Collections.Generic;
using System.Linq;
using NeoC.Game.Model;
using UnityEngine;

[CreateAssetMenu]
public class MasterOccupiedRange : ScriptableObject
{
    public List<SquareModel> occupiedRange;

    public IEnumerable<SquareModel> MovableSquares(SquareModel offset)
    {
        return occupiedRange.Select(s => s + offset);
    }

}
