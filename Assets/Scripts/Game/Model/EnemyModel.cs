using System.Collections.Generic;
using UniRx;

namespace NeoC.Game.Model
{
    public class EnemyModel : PieceModelBase
    {
        private readonly MasterEnemy masterEnemy;
        public ReactiveProperty<SquareModel> CurrentRotation { get; }
        public BoolReactiveProperty Dead { get; } = new BoolReactiveProperty(false);

        public EnemyModel(MasterEnemy masterEnemy, SquareModel initialSquare, SquareModel initialRotation)
            : base(initialSquare, masterEnemy.occupiedRange)
        {
            this.masterEnemy = masterEnemy;
            CurrentRotation = new ReactiveProperty<SquareModel>(initialRotation);
        }

        public void Move(int index)
        {
            if (Dead.Value)
            {
                return;
            }
            masterEnemy.Move(index, Move, Rotate);
        }

        private void Move(SquareModel delta)
        {
            delta = delta.Rotate(CurrentRotation.Value);
            CurrentSquare.Value += delta;
        }

        private void Rotate(SquareModel delta)
        {
            CurrentRotation.Value = CurrentRotation.Value.Rotate(delta);
        }

        public void Kill()
        {
            Dead.Value = true;
        }

        public override IEnumerable<SquareModel> OccupiedSquares()
        {
            if (Dead.Value)
            {
                return new SquareModel[] { };
            }
            return masterOccupiedRange.OccupiedSquares(CurrentSquare.Value, CurrentRotation.Value);
        }
    }
}
