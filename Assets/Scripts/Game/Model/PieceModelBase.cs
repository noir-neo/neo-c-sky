using UniRx;

namespace NeoC.Game.Model
{
    public abstract class PieceModelBase
    {
        public ReactiveProperty<SquareModel> CurrentSquare { get; }

        protected PieceModelBase() : this(new SquareModel())
        {
        }

        protected PieceModelBase(SquareModel initialSquare)
        {
            CurrentSquare = new ReactiveProperty<SquareModel>(initialSquare);
        }

        public virtual void MoveTo(SquareModel square)
        {
            CurrentSquare.Value = square;
        }
    }
}
