
using UnityEngine;

namespace NeoC.Game.Model
{
    public class EnemyModel : PieceModelBase
    {
        private readonly MasterEnemy masterEnemy;
        private SquareModel rotation;

        public EnemyModel(SquareModel initialSquare, MasterEnemy masterEnemy) : base(initialSquare)
        {
            this.masterEnemy = masterEnemy;
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
