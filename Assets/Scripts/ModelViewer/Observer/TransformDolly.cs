using UnityEngine;

namespace ModelViewer.Observer
{
    public sealed class TransformDolly : PinchObserverBase
    {
        [SerializeField] Transform dollyRoot;
        [SerializeField] private Vector3 axis;
        [SerializeField] private Vector3 minPosition;
        [SerializeField] private Vector3 maxPosition;

        protected override void OnPinch(float magnitude)
        {
            var pos = dollyRoot.localPosition;
            pos += axis * magnitude;
            dollyRoot.localPosition = pos.Clamp(minPosition, maxPosition);
        }
    }
}