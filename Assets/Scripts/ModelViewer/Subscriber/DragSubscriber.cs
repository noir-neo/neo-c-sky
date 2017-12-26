using System.Collections.Generic;
using System.Linq;
using ModelViewer.Interface;
using UniRx;
using UnityEngine;
using Zenject;

namespace ModelViewer.Subscriber
{
    public class DragSubscriber : MonoBehaviour
    {
        [Inject] private IDragPublisher dragPublisher;
        [Inject] private List<IDragBehaviour> behaviours;

        void Start()
        {
            dragPublisher.OnDragsAsObservable()
                .Subscribe(OnDrag)
                .AddTo(this);
        }

        private void OnDrag(IEnumerable<Vector2> deltas)
        {
            var pointerCount = deltas.Count();
            var delta = deltas.Aggregate((n, next) => Vector2.Lerp(n, next, 0.5f));

            foreach (var behaviour in behaviours)
            {
                if (behaviour.PointerCount == pointerCount)
                {
                    behaviour.OnDrag(delta);
                }
            }
        }
    }
}