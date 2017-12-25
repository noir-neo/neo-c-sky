using ModelViewer.Interface;
using UnityEngine;

namespace ModelViewer.Behaviour
{
    public sealed class TransformMover : MonoBehaviour, IDragBehaviour
    {
        [SerializeField] Transform moveRoot;
        [SerializeField] private float speed;
        [SerializeField] private Vector4 movableRage;

        void IDragBehaviour.OnDrag(Vector2 delta)
        {
            moveRoot.Move(delta * speed);
            moveRoot.position = moveRoot.position.Clamp(movableRage);
        }
    }
}
