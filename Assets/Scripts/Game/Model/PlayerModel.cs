using System.Collections.Generic;
using UniRx;

namespace NeoC.Game.Model
{
    public class PlayerModel : PieceModelBase
    {
        private readonly MasterMovableRange masterMovableRange;
        public IntReactiveProperty MoveCount { get; }

        public PlayerModel(MasterMovableRange masterMovableRange, SquareModel initialSquare) : base(initialSquare)
        {
            this.masterMovableRange = masterMovableRange;
            MoveCount = new IntReactiveProperty(-1);
        }

        public override void MoveTo(SquareModel square)
        {
            base.MoveTo(square);
            MoveCount.Value++;
        }

        public IEnumerable<SquareModel> MovableSquares()
        {
            return masterMovableRange.MovableSquares(CurrentSquare.Value);
        }
    }
}
