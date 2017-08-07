using NeoC.Game.Board;
using UnityEngine;

[CreateAssetMenu]
public class SquareState : ScriptableObject
{
    [SerializeField] private SquareStates state;
    public SquareStates State => state;
    public enum SquareStates
    {
        Default,
        Selectable,
        Selected,
        Occupied,
        Intersected
    }

    [SerializeField] private bool selectable;
    [SerializeField] private Color32 color;

    public void UpdateState(Square square)
    {
        square.UpdateState(selectable, color);
    }
}
