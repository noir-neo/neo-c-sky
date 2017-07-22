namespace NeoC.Game.Model
{
    public class EnemyModel : PieceModelBase
    {
        private readonly MasterEnemy masterEnemy;
        private SquareModel rotation;

        public EnemyModel(MasterEnemy masterEnemy, SquareModel initialSquare, SquareModel initialRotation)
            : base(initialSquare)
        {
            this.masterEnemy = masterEnemy;
            rotation = initialRotation;
        }

        public void Move()
        {
            masterEnemy.Move(0, Move, Rotate);
        }

        public void Move(SquareModel delta)
        {
            currentSquare.Value += delta.Rotate(rotation);
        }

        private void Rotate(SquareModel delta)
        {
            rotation.Rotate(delta);
        }
    }
}
