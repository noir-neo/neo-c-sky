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
        private IReadOnlyDictionary<EnemyModel, PieceMover> enemyModelMovers;
        private BoardMedel boardModel;

        [Inject] private PieceMover playerMover;
        [Inject] private Board.Board board;

        [SerializeField] private MasterLevel level;

        void Start()
        {
            InitBoard();
            InitPlayer();
            InitEnemies();
            InitObserver();
        }

        private void InitBoard()
        {
            boardModel = level.BoardMedel(GetSquarePosition);
            board.CreateSquares(boardModel.SquareModels);
            board.CreateGoal(boardModel.GoalSquare);
        }

        private void InitPlayer()
        {
            playerModel = level.PlayerModel();
        }

        private void InitEnemies()
        {
            enemyModelMovers = level.EnemyModelMovers(GetSquarePosition);
        }

        private void InitObserver()
        {
            board.OnClickSquaresAsObservable()
                .Where(s => OccupiedSquares(playerModel).Contains(s))
                .Subscribe(playerModel.MoveTo);

            board.OnDownSquaresAsObservable()
                .Where(s => OccupiedSquares(playerModel).Contains(s))
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
                    .Subscribe(s => enemyModelMoverPair.Value.MoveTo(GetSquarePosition(s)));
                enemyModelMoverPair.Key.CurrentRotation
                    .Subscribe(s => enemyModelMoverPair.Value.LookRotation(s.LookRotation()));
            }
        }

        private void OnPlayerCoordinateChanged(SquareModel position)
        {
            board.UpdateStates();
            board.UpdateState(position, SquareState.SquareStates.Selected);
            playerMover.MoveTo(GetSquarePosition(position), UpdateStates);
        }

        private Vector3 GetSquarePosition(SquareModel squareModel)
        {
            return board.GetSquarePosition(squareModel);
        }

        private void UpdateStates()
        {
            board.UpdateStates();
            var playerOccupied = OccupiedSquares(playerModel);
            var enemyOccupied = OccupiedSquares(enemyModelMovers.Keys);
            board.UpdateStates(playerOccupied, SquareState.SquareStates.Selectable);
            board.UpdateStates(enemyOccupied, SquareState.SquareStates.Occupied);
            var intersected = playerOccupied.Intersect(enemyOccupied);
            board.UpdateStates(intersected, SquareState.SquareStates.Intersected);
        }

        private IEnumerable<SquareModel> OccupiedSquares(IEnumerable<PieceModelBase> pieceModels)
        {
            return boardModel.IntersectedSquares(
                pieceModels.SelectMany(p => p.OccupiedSquares())
                    .Distinct());
        }

        private IEnumerable<SquareModel> OccupiedSquares(PieceModelBase pieceModel)
        {
            return boardModel.IntersectedSquares(pieceModel.OccupiedSquares());
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