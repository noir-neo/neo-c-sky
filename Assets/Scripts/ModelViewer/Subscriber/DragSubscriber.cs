using System.Collections.Generic;
using ModelViewer.EventData;
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

        private void OnDrag(DragEventData dragEventData)
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