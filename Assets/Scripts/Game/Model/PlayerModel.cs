using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace NeoC.Game.Model
{
    public class PlayerModel
    {
        private readonly IList<SquareModel> movableRange = new List<SquareModel>
        {
            new SquareModel(-1, 1),  new SquareModel(0, 1),  new SquareModel(1, 1),
            new SquareModel(-1, 0),                          new SquareModel(1, 0),
            new SquareModel(-1, -1), new SquareModel(0, -1), new SquareModel(1, -1)
        };

        public ReactiveProperty<SquareModel> currentSquare { get; private set; }

        public PlayerModel()
        {
            currentSquare = new ReactiveProperty<SquareModel>();
        }

        public void UpdateCoordinate(SquareModel squareModel)
        {
            currentSquare.Value = squareModel;
        }

        public IEnumerable<SquareModel> MovableSquares()
        {
            return movableRange.Select(s => s + currentSquare.Value);
        }
    }
}
