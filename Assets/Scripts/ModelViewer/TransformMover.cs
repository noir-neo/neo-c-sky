using ModelViewer.Handler;
using UnityEngine;

namespace ModelViewer
{
    public class TransformMover : MonoBehaviour, IDragHandler
    {
        [SerializeField] Transform moveRoot;
        [SerializeField] private float speed;
        [SerializeField] private Vector4 movableRage;
        
        public void OnDrag(Vector2 delta)
        {
            moveRoot.Move(delta * speed);
            moveRoot.position = moveRoot.position.Clamp(movableRage);
        }
    }
}
