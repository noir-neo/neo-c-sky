using NeoC.Game.Model;
using UnityEngine;

[CreateAssetMenu]
public class MasterLevel : ScriptableObject
{
    public SquareModel boardSize;
    public MasterMovableRange PlayerMovableRange;
    public SquareModel playerInitialSquare;

}
