using UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace ModelViewer
{
    public class ModelMover : MonoBehaviour
    {
        [SerializeField] Transform moveRoot;
        [SerializeField] private float speed;
        [SerializeField] private Vector4 movableRage;

        [Inject] private UIDragHandler dragHandler;

        void Start()
        {
            dragHandler.OnDragsDeltaAsObservable(2)
                .Subscribe(Move);
        }

        private void Move(Vector2 delta)
        {
            moveRoot.Move(delta * speed);
            moveRoot.position = moveRoot.position.Clamp(movableRage);
        }
    }
}