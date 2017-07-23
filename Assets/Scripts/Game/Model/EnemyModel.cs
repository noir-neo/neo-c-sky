using UniRx;

namespace NeoC.Game.Model
{
    public class EnemyModel : PieceModelBase
    {
        private readonly MasterEnemy masterEnemy;
        public ReactiveProperty<SquareModel> CurrentRotation { get; }

        public EnemyModel(MasterEnemy masterEnemy, SquareModel initialSquare, SquareModel initialRotation)
            : base(initialSquare)
        {
            this.masterEnemy = masterEnemy;
            CurrentRotation = new ReactiveProperty<SquareModel>(initialRotation);
        }

        public void Move(int index)
        {
            masterEnemy.Move(index, Move, Rotate);
        }

        public void Move(SquareModel delta)
        {
            delta = delta.Rotate(CurrentRotation.Value);
            CurrentSquare.Value += delta;
        }

        private void Rotate(SquareModel delta)
        {
            CurrentRotation.Value = CurrentRotation.Value.Rotate(delta);
        }
    }
}
