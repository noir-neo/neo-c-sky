using System.Collections.Generic;
using System.Linq;
using ModelViewer.Handler;
using NeoC.Game.Model;
using UniRx;
using UnityEngine;
using Zenject;

namespace NeoC.Game
{
    public class GamePresenter : MonoBehaviour
    {
        private PlayerModel playerModel;
        private BoardMedel boardMedel;

        [Inject] private PlayerMover playerMover;
        [Inject] private Board.Board board;

        void Start()
        {
            InitModels();
            InitObservers();
        }

        private void InitModels()
        {
            playerModel = new PlayerModel();
            // TODO: MasterData
            boardMedel = new BoardMedel(3, 3);
        }

        private void InitObservers()
        {
            board.OnClickSquaresAsObservable()
                .Where(s => MovableSquares(playerModel).Contains(s))
                .Subscribe(playerModel.UpdateCoordinate);

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
            return boardMedel.IntersectedSquares(playerModel.MovableSquares());
        }
    }
}