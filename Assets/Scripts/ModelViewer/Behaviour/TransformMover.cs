using UnityEngine;

namespace ModelViewer.Behaviour
{
    public sealed class TransformMover : DragObserverBase
    {
        [SerializeField] Transform moveRoot;
        [SerializeField] private float speed;
        [SerializeField] private Vector4 movableRage;

        protected override void OnDrag(Vector2 delta)
        {
            moveRoot.Move(delta * speed);
            moveRoot.position = moveRoot.position.Clamp(movableRage);
        }
    }
}
