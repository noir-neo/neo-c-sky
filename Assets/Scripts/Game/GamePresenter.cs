using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

namespace NeoC.Game
{
    public class GamePresenter : MonoBehaviour
    {
        [Inject] private UIDragHandler dragHandler;
        [Inject] private Attacker attacker;
        [Inject] private PlayerMover playerMover;

        void Start()
        {
            dragHandler.OnDragsAsObservable(1)
                .TakeUntil(dragHandler.OnEndDragAsObservable())
                .RepeatUntilDestroy(this)
                .Select(x => x.Single().delta)
                .Subscribe(playerMover.Move);

            attacker.OnAttackAsObservable()
                .Subscribe(saboten => saboten.Break());
        }
    }
}