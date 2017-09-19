using System.Collections.Generic;
using System.Linq;
using NeoC.Game.Board;
using NeoC.Game.Model;
using NeoC.UI;
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
        [Inject] private UIPresenter uiPresenter;

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
            boardModel = level.BoardModel();
            board.CreateSquares(boardModel.SquareModels);
            board.CreateGoal(boardModel.GoalSquare);
        }

        private void InitPlayer()
        {
            playerModel = level.PlayerModel();
        }

        private void InitEnemies()
        {
            enemyModelMovers = level.EnemyModels()
                .ToDictionary(
                    x => x.Item1,
                    x => board.Instantiate(x.Item2, x.Item1));
        }

        private void InitObserver()
        {
            board.OnClickSquaresAsObservable()
                .Where(s => OccupiedSquares(playerModel).Contains(s))
                .Subscribe(playerModel.MoveTo)
                .AddTo(this);

            board.OnClickSquaresAsObservable()
                .First()
                .Subscribe(_ => uiPresenter.Close<LevelSelectWindow>())
                .AddTo(this);

            board.OnDownSquaresAsObservable()
                .Where(s => OccupiedSquares(playerModel).Contains(s))
                .Subscribe(OnPreselected)
                .AddTo(this);

            board.OnExitSquaresAsObservable()
                .Subscribe(_ => UpdateStates())
                .AddTo(this);

            playerModel.OnMoveAsObservable()
                .First()
                .Select(x => x.Item1)
                .Subscribe(InitialPlayer)
                .AddTo(this);
            playerModel.OnMoveAsObservable()
                .Skip(1)
                .Subscribe(OnPlayerCoordinateChanged)
                .AddTo(this);

            foreach (var enemyModelMoverPair in enemyModelMovers)
            {
                enemyModelMoverPair.Key.CurrentSquare
                    .Subscribe(s => enemyModelMoverPair.Value.MoveTo(GetSquarePosition(s)))
                    .AddTo(this);
                enemyModelMoverPair.Key.CurrentRotation
                    .Subscribe(s => enemyModelMoverPair.Value.LookRotation(s.LookRotation()))
                    .AddTo(this);
                enemyModelMoverPair.Key.Dead
                    .Where(d => d)
                    .Subscribe(s => enemyModelMoverPair.Value.Kill())
                    .AddTo(this);
            }
        }

        private void OnPreselected(SquareModel position)
        {
            board.UpdateState(position, SquareState.SquareStates.Selected);
            playerMover.LookAt(GetSquarePosition(position));
        }

        // TODO: to Observable
        private void OnPlayerCoordinateChanged(Tuple<SquareModel, int> positionIndex)
        {
            var dyingEnemies = enemyModelMovers.Keys.Where(m => m.CurrentSquare.Value == positionIndex.Item1);
            foreach (var dyingEnemy in dyingEnemies)
            {
                dyingEnemy.Kill();
            }

            if (MovePlayer(positionIndex.Item1, dyingEnemies.Any()))
            {
                uiPresenter.OpenResult(true);
            }
            else if (MoveEnemies(positionIndex.Item2))
            {
                uiPresenter.OpenResult(false);
            }
        }

        private void InitialPlayer(SquareModel position)
        {
            playerMover.PositionTo(GetSquarePosition(position));
            UpdateStates();
        }

        private bool MovePlayer(SquareModel position, bool killing)
        {
            board.UpdateState(position, SquareState.SquareStates.Selected);
            board.UpdateStatesExcept(new []{ position });
            playerMover.MoveTo(GetSquarePosition(position), UpdateStates);

            return boardModel.IsGoal(playerModel.CurrentSquare.Value);
        }

        private bool MoveEnemies(int index)
        {
            foreach (var enemyModel in enemyModelMovers.Keys)
            {
                enemyModel.Move(index);
            }

            var enemyOccupied = OccupiedSquares(enemyModelMovers.Keys);
            return enemyOccupied.Contains(playerModel.CurrentSquare.Value);
        }

        private Vector3 GetSquarePosition(SquareModel squareModel)
        {
            return board.GetSquarePosition(squareModel);
        }

        private void UpdateStates()
        {
            var playerOccupied = OccupiedSquares(playerModel);
            var enemyOccupied = OccupiedSquares(enemyModelMovers.Keys);

            board.UpdateStates(playerOccupied, SquareState.SquareStates.Selectable);
            board.UpdateStates(enemyOccupied, SquareState.SquareStates.Occupied);
            var intersected = playerOccupied.Intersect(enemyOccupied);
            board.UpdateStates(intersected, SquareState.SquareStates.Intersected);
            board.UpdateStatesExcept(playerOccupied.Union(enemyOccupied));
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
    }
}