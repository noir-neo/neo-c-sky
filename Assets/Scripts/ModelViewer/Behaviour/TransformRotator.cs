using ModelViewer.Interface;
using UnityEngine;

namespace ModelViewer.Behaviour
{
    public sealed class TransformRotator : MonoBehaviour, IDragBehaviour
    {
        [SerializeField] Transform xRotateRoot;
        [SerializeField] Transform yRotateRoot;
        [SerializeField] private float speed;
        [SerializeField] private float minXAngle;
        [SerializeField] private float maxXAngle;

        void IDragBehaviour.OnDrag(Vector2 delta)
        {
            delta *= speed;
            yRotateRoot.Rotate(Vector3.up, -delta.x);
            xRotateRoot.Rotate(Vector3.right, delta.y, minXAngle, maxXAngle);
        }
    }
}
