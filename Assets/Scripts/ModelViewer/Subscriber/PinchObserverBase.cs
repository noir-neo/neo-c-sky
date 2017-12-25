using ModelViewer.Publisher;
using UniRx;
using UnityEngine;
using Zenject;

namespace ModelViewer.Subscriber
{
    public abstract class PinchObserverBase : MonoBehaviour
    {
        [SerializeField] private float ignoreAngleThreshold;

        [Inject] private UIDragHandler dragHandler;

        void Start()
        {
            dragHandler.OnPinchAsObservable(ignoreAngleThreshold)
                .Subscribe(OnPinch);
        }

        protected abstract void OnPinch(float magnitude);
    }
}