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
                .Select(MovePlayerAsObservable)
                .Switch()
                .Subscribe()
                .AddTo(this);

            // TODO: ゲームロジックを Model 層に
            playerModel.OnMoveAsObservable()
                .Select(x => Tuple.Create(boardModel.IsGoal(x.Item1), x.Item2))
                .Subscribe(x =>
                {
                    if (x.Item1)
                    {
                        uiPresenter.OpenResult(true);
                        return;
                    }
                    MoveEnemies(x.Item2);
                })
                .AddTo(this);

            enemyModelMovers.Select(e => e.Key.CurrentRotation
                    .Select(s => s.LookRotation())
                    .Select(r => e.Value.LookRotationAsObservable(r))
                    .Switch())
                .Merge()
                .Subscribe()
                .AddTo(this);

            enemyModelMovers.Select(e => e.Key.CurrentSquare
                    .Select(GetSquarePosition)
                    .Select(p => e.Value.MoveToAsObservable(p))
                    .Switch())
                .Merge()
                .Subscribe()
                .AddTo(this);

            enemyModelMovers.Select(e => e.Key.Dead
                    .Where(x => x)
                    .Select(_ => e.Value))
                .Merge()
                .Subscribe(m => m.Kill())
                .AddTo(this);

            enemyModelMovers.Select(e => e.Key.CurrentRotation
                    .CombineLatest(e.Key.CurrentSquare, (r, p) => e.Key))
                .Merge()
                .Select(OccupiedSquares)
                .Where(o => o.Contains(playerModel.CurrentSquare.Value))
                .First()
                .Subscribe(_ => uiPresenter.OpenResult(false))
                .AddTo(this);
        }

        private void OnPreselected(SquareModel position)
        {
            board.UpdateState(position, SquareState.SquareStates.Selected);
            playerMover.LookAt(GetSquarePosition(position));
        }

        private IObservable<Unit> MovePlayerAsObservable(Tuple<SquareModel, int> positionIndex)
        {
            board.UpdateState(positionIndex.Item1, SquareState.SquareStates.Selected);
            board.UpdateStatesExcept(new []{ positionIndex.Item1 });

            KillEnemies(enemyModelMovers.Keys, positionIndex.Item1);
            return playerMover.MoveToAsObservable(GetSquarePosition(positionIndex.Item1));
        }

        private static void KillEnemies(IEnumerable<EnemyModel> enemyModels, SquareModel position)
        {
            var dyingEnemies = enemyModels.Where(m => m.CurrentSquare.Value == position);
            foreach (var dyingEnemy in dyingEnemies)
            {
                dyingEnemy.Kill();
            }
        }

        private void InitialPlayer(SquareModel position)
        {
            playerMover.PositionTo(GetSquarePosition(position));
            UpdateStates();
        }

        private void MoveEnemies(int index)
        {
            foreach (var enemyModel in enemyModelMovers.Keys)
            {
                enemyModel.Move(index);
            }
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