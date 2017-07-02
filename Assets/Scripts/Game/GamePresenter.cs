using System.Linq;
using ModelViewer.Handler;
using UniRx;
using UnityEngine;
using Zenject;

namespace NeoC.Game
{
    public class GamePresenter : MonoBehaviour
    {
        [Inject] private PlayerMover playerMover;

        void Start()
        {

        }
    }
}