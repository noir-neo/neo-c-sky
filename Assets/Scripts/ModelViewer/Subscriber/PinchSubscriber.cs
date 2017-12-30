using System.Collections.Generic;
using System.Linq;
using ModelViewer.Interface;
using UniRx;
using UnityEngine;
using Zenject;

namespace ModelViewer.Subscriber
{
    public class PinchSubscriber : MonoBehaviour
    {
        [Inject]
        public void Init(List<IPinchPublisher> publishers, List<IPinchBehaviour> behaviours)
        {
            publishers.Select(p => p.OnPinchAsObservable())
                .Merge()
                .Subscribe(d => OnPinch(d, behaviours))
                .AddTo(this);
        }

        private static void OnPinch(float magnitude, IEnumerable<IPinchBehaviour> behaviours)
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.OnPinch(magnitude);
            }
        }
    }
}