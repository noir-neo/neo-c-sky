using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using Zenject;

namespace NeoC.ModelViewer
{
    public class ModelMover : MonoBehaviour
    {
        [SerializeField] Transform moveRoot;
        [SerializeField] private float speed;
        [SerializeField] private Vector4 movableRage;

        [Inject] private UIDragHandler dragHandler;

        void Start()
        {
            dragHandler.OnDragsAsObservable(2)
                .TakeUntil(dragHandler.OnEndDragAsObservable())
                .RepeatUntilDestroy(this)
                .Select(x => x.Select(p => p.delta))
                .Select(x => x.Aggregate((n, next) => Vector2.Lerp(n, next, 0.5f)))
                .Subscribe(Move);
        }

        private void Move(Vector2 delta)
        {
            moveRoot.Move(delta * speed);
            moveRoot.position = moveRoot.position.Clamp(movableRage);
        }
    }
}
