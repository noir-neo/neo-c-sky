using System.Collections.Generic;

namespace NeoC.Game.Model
{
    public class PlayerModel : PieceModelBase
    {
        private readonly MasterMovableRange masterMovableRange;

        public PlayerModel(SquareModel initialSquare, MasterMovableRange masterMovableRange) : base(initialSquare)
        {
            this.masterMovableRange = masterMovableRange;
        }

        public IEnumerable<SquareModel> MovableSquares()
        {
            return masterMovableRange.MovableSquares(currentSquare.Value);
        }
    }
}
