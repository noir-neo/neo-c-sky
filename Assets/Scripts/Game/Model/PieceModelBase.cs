using System.Collections.Generic;
using UniRx;

namespace NeoC.Game.Model
{
    public abstract class PieceModelBase
    {
        public ReactiveProperty<SquareModel> CurrentSquare { get; }
        protected readonly MasterOccupiedRange masterOccupiedRange;

        protected PieceModelBase() : this(new SquareModel(), new MasterOccupiedRange())
        {
        }

        protected PieceModelBase(SquareModel initialSquare, MasterOccupiedRange masterOccupiedRange)
        {
            CurrentSquare = new ReactiveProperty<SquareModel>(initialSquare);
            this.masterOccupiedRange = masterOccupiedRange;
        }

        public virtual void MoveTo(SquareModel square)
        {
            CurrentSquare.Value = square;
        }

        public virtual IEnumerable<SquareModel> OccupiedSquares()
        {
            return masterOccupiedRange.OccupiedSquares(CurrentSquare.Value);
        }
    }
}
