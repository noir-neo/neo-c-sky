using ModelViewer.Handler;
using UnityEngine;

namespace ModelViewer
{
    public class TransformRotator : MonoBehaviour, IDragsHandler
    {
        [SerializeField] Transform xRotateRoot;
        [SerializeField] Transform yRotateRoot;
        [SerializeField] private float speed;
        [SerializeField] private float minXAngle;
        [SerializeField] private float maxXAngle;
        
        public void OnDrag(Vector2 delta)
        {
            delta *= speed;
            yRotateRoot.Rotate(Vector3.up, -delta.x);
            xRotateRoot.Rotate(Vector3.right, delta.y, minXAngle, maxXAngle);
        }
    }
}
