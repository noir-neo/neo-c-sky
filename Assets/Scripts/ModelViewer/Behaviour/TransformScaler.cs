using UnityEngine;

namespace ModelViewer.Behaviour
{
    public sealed class TransformScaler : PinchObserverBase
    {
        [SerializeField] Transform scaleRoot;
        [SerializeField] private float speed;
        [SerializeField] private float minScale;
        [SerializeField] private float maxScale;

        protected override void OnPinch(float magnitude)
        {
            scaleRoot.Scale(magnitude * speed, minScale, maxScale);
        }
    }
}