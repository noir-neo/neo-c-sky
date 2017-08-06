using UniRx;

namespace NeoC.Game.Model
{
    public class PlayerModel : PieceModelBase
    {

        public IntReactiveProperty MoveCount { get; }

        public PlayerModel(SquareModel initialSquare, MasterOccupiedRange masterOccupiedRange)
            : base(initialSquare, masterOccupiedRange)
        {
            MoveCount = new IntReactiveProperty(-1);
        }

        public override void MoveTo(SquareModel square)
        {
            base.MoveTo(square);
            MoveCount.Value++;
        }
    }
}
