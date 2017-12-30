using System.Collections.Generic;
using System.Linq;
using ModelViewer.EventData;
using ModelViewer.Interface;
using UniRx;
using UnityEngine;
using Zenject;

namespace ModelViewer.Subscriber
{
    public class DragSubscriber : MonoBehaviour
    {
        [Inject]
        public void Init(List<IDragPublisher> publishers, List<IDragBehaviour> behaviours)
        {
            publishers.Select(p => p.OnDragsAsObservable())
                .Merge()
                .Subscribe(d => OnDrag(d, behaviours))
                .AddTo(this);
        }

        private static void OnDrag(DragEventData dragEventData, IEnumerable<IDragBehaviour> behaviours)
        {
            foreach (var behaviour in behaviours)
            {
                if (behaviour.PointerCount == dragEventData.PointerCount)
                {
                    behaviour.OnDrag(dragEventData.Delta);
                }
            }
        }
    }
}