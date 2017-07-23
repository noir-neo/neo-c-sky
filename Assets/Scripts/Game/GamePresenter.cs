﻿using System.Collections.Generic;
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
        private List<EnemyModel> enemyModels;
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
            enemyModels = level.EnemyModels();
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

            playerModel.currentSquare
                .Subscribe(OnPlayerCoordinateChanged);
        }

        private void OnPlayerCoordinateChanged(SquareModel coordinate)
        {
            board.UpdateSelectables();
            board.Highlight(coordinate);

            Vector2 xz;
            if (board.TryGetSquarePosition(coordinate, out xz))
            {
                playerMover.MoveTo(xz, UpdateSelectable);
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
    }
}