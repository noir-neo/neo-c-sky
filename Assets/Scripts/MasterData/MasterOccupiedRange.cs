using System.Collections.Generic;
using System.Linq;
using NeoC.Game.Model;
using UnityEngine;

[CreateAssetMenu]
public class MasterOccupiedRange : ScriptableObject
{
    public List<SquareModel> occupiedRange;

    public IEnumerable<SquareModel> OccupiedSquares(SquareModel offset)
    {
        return occupiedRange.Select(s => s + offset);
    }

    public IEnumerable<SquareModel> OccupiedSquares(SquareModel offset, SquareModel rotation)
    {
        return occupiedRange.Select(s => s.Rotate(rotation) + offset);
    }
}
