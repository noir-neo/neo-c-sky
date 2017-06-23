using ModelViewer.UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace ModelViewer
{
    public abstract class DragObserverBase : MonoBehaviour
    {
        [SerializeField] private int pointerCount;

        [Inject] private UIDragHandler dragHandler;

        void Start()
        {
            dragHandler.OnDragsDeltaAsObservable(pointerCount)
                .Subscribe(OnDrag);
        }

        protected abstract void OnDrag(Vector2 delta);
    }
}