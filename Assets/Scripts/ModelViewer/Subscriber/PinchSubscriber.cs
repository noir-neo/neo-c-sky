using System.Collections.Generic;
using ModelViewer.Interface;
using ModelViewer.Publisher;
using UniRx;
using UnityEngine;
using Zenject;

namespace ModelViewer.Subscriber
{
    public class PinchSubscriber : MonoBehaviour
    {
        [SerializeField] private float ignoreAngleThreshold;

        [Inject] private UIDragHandler dragHandler;
        [Inject] private List<IPinchBehaviour> behaviours;

        void Start()
        {
            dragHandler.OnPinchAsObservable(ignoreAngleThreshold)
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