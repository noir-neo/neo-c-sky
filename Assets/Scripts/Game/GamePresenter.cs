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
        private PlayerMedel playerModel;

        [Inject] private PlayerMover playerMover;
        [Inject] private Board.Board board;

        void Start()
        {
            InitModels();
            InitObservers();
        }

        private void InitModels()
        {
            playerModel = new PlayerMedel();
        }

        private void InitObservers()
        {
            board.OnClickSquaresAsObservable()
                .Subscribe(playerModel.UpdateCoordinate);

            playerModel.currentCoordinate
                .Subscribe(OnPlayerCoordinateChanged);
        }

        private void OnPlayerCoordinateChanged(BoardCoordinate coordinate)
        {
            Vector2 xz;
            if (board.TryGetSquarePosition(coordinate, out xz))
            {
                playerMover.MoveTo(xz);
            }
        }
    }
}