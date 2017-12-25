using ModelViewer.Interface;
using UnityEngine;

namespace ModelViewer.Behaviour
{
    public sealed class TransformDolly : MonoBehaviour, IPinchBehaviour
    {
        [SerializeField] Transform dollyRoot;
        [SerializeField] private Vector3 axis;
        [SerializeField] private Vector3 minPosition;
        [SerializeField] private Vector3 maxPosition;

        void IPinchBehaviour.OnPinch(float magnitude)
        {
            var pos = dollyRoot.localPosition;
            pos += axis * magnitude;
            dollyRoot.localPosition = pos.Clamp(minPosition, maxPosition);
        }
    }
}