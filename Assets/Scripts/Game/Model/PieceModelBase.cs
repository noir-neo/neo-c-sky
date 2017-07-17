using UniRx;

namespace NeoC.Game.Model
{
    public abstract class PieceModelBase
    {
        public ReactiveProperty<SquareModel> currentSquare { get; private set; }

        protected PieceModelBase() : this(new SquareModel())
        {
        }

        protected PieceModelBase(SquareModel initialSquare)
        {
            currentSquare = new ReactiveProperty<SquareModel>(initialSquare);
        }

        public virtual void MoveTo(SquareModel square)
        {
            currentSquare.Value = square;
        }
    }
}
