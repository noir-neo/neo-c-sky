using System.Collections.Generic;
using System.Linq;

namespace NeoC.Game.Model
{
    public class BoardMedel
    {
        private readonly List<SquareModel> squareModels;

        public BoardMedel(int x, int y)
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

    }
}
