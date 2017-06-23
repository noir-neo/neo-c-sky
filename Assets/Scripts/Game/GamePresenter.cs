using System.Linq;
using ModelViewer.UI;
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
        [Inject] private SabotenGenerator sabotenGenerator;

        void Start()
        {
            dragHandler.OnDragsAsObservable(1)
                .TakeUntil(dragHandler.OnEndDragAsObservable())
                .RepeatUntilDestroy(this)
                .Select(x => x.Single().delta)
                .Subscribe(playerMover.Move);

            attacker.OnAttackAsObservable()
                .Subscribe(OnAttack);
        }

        private void OnAttack(Saboten saboten)
        {
            saboten.Break();
            var position = saboten.transform.position;
            sabotenGenerator.Generate(RandomVector3(
                position.x - 30f, position.x + 30f, 
                0, 0,
                position.z - 30f, position.z + 30f));
        }

        private static Vector3 RandomVector3(float xFrom, float xTo, float yFrom, float yTo, float zFrom, float zTo)
        {
            return new Vector3(
                Random.Range(xFrom, xTo),
                Random.Range(yFrom, yTo),
                Random.Range(zFrom, zTo));
        }
    }
}