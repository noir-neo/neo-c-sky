using System.Collections.Generic;
using UniRx;

namespace NeoC.Game.Model
{
    public class PlayerModel : PieceModelBase
    {
        private readonly MasterOccupiedRange masterOccupiedRange;
        public IntReactiveProperty MoveCount { get; }

        public PlayerModel(MasterOccupiedRange masterOccupiedRange, SquareModel initialSquare) : base(initialSquare)
        {
            this.masterOccupiedRange = masterOccupiedRange;
            MoveCount = new IntReactiveProperty(-1);
        }

        public override void MoveTo(SquareModel square)
        {
            base.MoveTo(square);
            MoveCount.Value++;
        }

        public IEnumerable<SquareModel> MovableSquares()
        {
            return masterOccupiedRange.MovableSquares(CurrentSquare.Value);
        }
    }
}
