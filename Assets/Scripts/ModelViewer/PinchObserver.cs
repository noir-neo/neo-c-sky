using ModelViewer.Handler;
using ModelViewer.UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace ModelViewer
{
    public class PinchObserver : MonoBehaviour
    {
        [SerializeField] private IPinchHandler handler;
        [SerializeField] private float ignoreAngleThreshold;

        [Inject] private UIDragHandler dragHandler;

        void Start()
        {
            dragHandler.OnPinchAsObservable(ignoreAngleThreshold)
                .Subscribe(handler.OnPinch);
        }
    }
}