using System.Collections.Generic;
using ModelViewer.Interface;
using UniRx;
using UnityEngine;
using Zenject;

namespace ModelViewer.Subscriber
{
    public class PinchSubscriber : MonoBehaviour
    {
        [Inject] private IPinchPublisher pinchPublisher;
        [Inject] private List<IPinchBehaviour> behaviours;

        void Start()
        {
            pinchPublisher.OnPinchAsObservable()
                .Subscribe(OnPinch)
                .AddTo(this);
        }

        private void OnPinch(float magnitude)
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.OnPinch(magnitude);
            }
        }
    }
}