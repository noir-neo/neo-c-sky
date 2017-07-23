using System.Collections.Generic;
using System.Linq;
using NeoC.Game.Model;
using UniRx;
using UnityEngine;
using Zenject;

namespace NeoC.Game
{
    public class GamePresenter : MonoBehaviour
    {
        private PlayerModel playerModel;
        private Dictionary<EnemyModel, PieceMover> enemyModelMovers;
        private BoardMedel boardModel;

        [Inject] private PieceMover playerMover;
        [Inject] private Board.Board board;

        [SerializeField] private MasterLevel level;

        void Start()
        {
            InitModels();
            InitViews();
            InitObservers();
        }

        private void InitModels()
        {
            playerModel = level.PlayerModel();
            enemyModelMovers = level.EnemyModelMovers();
            boardModel = level.BoardMedel();
        }

        private void InitViews()
        {
            board.CreateSquares(boardModel.SquareModels);
        }

        private void InitObservers()
        {
            board.OnClickSquaresAsObservable()
                .Where(s => MovableSquares(playerModel).Contains(s))
                .Subscribe(playerModel.MoveTo);

            board.OnDownSquaresAsObservable()
                .Where(s => MovableSquares(playerModel).Contains(s))
                .Select(s => board.GetSquare(s))
                .Where(s => s != null)
                .Select(s => s.Position())
                .Subscribe(playerMover.LookAt);

            playerModel.CurrentSquare
                .Subscribe(OnPlayerCoordinateChanged);

            playerModel.MoveCount
                .Subscribe(MoveEnemies);

            foreach (var enemyModelMoverPair in enemyModelMovers)
            {
                enemyModelMoverPair.Key.CurrentSquare
                    .Subscribe(s => MoveTo(enemyModelMoverPair.Value, s));
                enemyModelMoverPair.Key.CurrentRotation
                    .Subscribe(s => enemyModelMoverPair.Value.LookRotation(s));
            }
        }

        private void OnPlayerCoordinateChanged(SquareModel position)
        {
            board.UpdateSelectables();
            board.Highlight(position);

            MoveTo(playerMover, position);
        }

        private void MoveTo(PieceMover mover, SquareModel position)
        {
            Vector2 xz;
            if (board.TryGetSquarePosition(position, out xz))
            {
                mover.MoveTo(xz, UpdateSelectable);
            }
        }

        private void UpdateSelectable()
        {
            board.UpdateSelectables(MovableSquares(playerModel));
        }

        private IEnumerable<SquareModel> MovableSquares(PlayerModel playerModel)
        {
            return boardModel.IntersectedSquares(playerModel.MovableSquares());
        }

        private void MoveEnemies(int index)
        {
            foreach (var enemyModel in enemyModelMovers.Keys)
            {
                enemyModel.Move(index);
            }
        }
    }
}