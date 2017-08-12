using UniRx;

namespace NeoC.Game.Model
{
    public class PlayerModel : PieceModelBase
    {

        private IntReactiveProperty MoveCount { get; }

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

        public IObservable<Tuple<SquareModel, int>> OnMoveAsObservable()
        {
            return CurrentSquare.Zip(MoveCount, (square, count) => new Tuple<SquareModel, int>(square, count));
        }
    }
}
