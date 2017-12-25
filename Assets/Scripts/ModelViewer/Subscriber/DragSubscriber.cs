using System.Collections.Generic;
using ModelViewer.Interface;
using ModelViewer.Publisher;
using UniRx;
using UnityEngine;
using Zenject;

namespace ModelViewer.Subscriber
{
    public class DragSubscriber : MonoBehaviour
    {
        [SerializeField] private int pointerCount;

        [Inject] private UIDragHandler dragHandler;
        [Inject] private List<IDragBehaviour> behaviours;

        void Start()
        {
            dragHandler.OnDragsDeltaAsObservable(pointerCount)
                .Subscribe(OnDrag)
                .AddTo(this);
        }

        private void OnDrag(Vector2 delta)
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.OnDrag(delta);
            }
        }
    }
}