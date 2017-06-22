using ModelViewer.Handler;
using ModelViewer.UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace ModelViewer
{
    public class DragObserver : MonoBehaviour
    {
        [SerializeField] private IDragsHandler handler;
        [SerializeField] private int pointerCount;

        [Inject] private UIDragHandler dragHandler;

        void Start()
        {
            dragHandler.OnDragsDeltaAsObservable(pointerCount)
                .Subscribe(handler.OnDrag);
        }
    }
}