using System.Collections.Generic;
using System.Linq;

namespace NeoC.Game.Model
{
    public class BoardModel
    {
        private readonly List<SquareModel> squareModels;
        public List<SquareModel> SquareModels => squareModels;
        private readonly SquareModel goalSquare;
        public SquareModel GoalSquare => goalSquare;

        public BoardModel(SquareModel boardSize, SquareModel goalSquare) : this(boardSize.X, boardSize.Y)
        {
            this.goalSquare = goalSquare;
        }

        public BoardModel(int x, int y)
        {
            squareModels = new List<SquareModel>();
            for (int iy = 0; iy < y; iy++)
            {
                for (int ix = 0; ix < x; ix++)
                {
                    squareModels.Add(new SquareModel(ix, iy));
                }
            }
        }

        public IEnumerable<SquareModel> IntersectedSquares(IEnumerable<SquareModel> squares)
        {
            return squareModels.Intersect(squares);
        }

        public bool IsGoal(SquareModel squareModel)
        {
            return squareModel == goalSquare;
        }
    }
}
