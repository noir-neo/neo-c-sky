using UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace ModelViewer
{
    public class ModelRotator : MonoBehaviour
    {
        [SerializeField] Transform xRotateRoot;
        [SerializeField] Transform yRotateRoot;
        [SerializeField] private float speed;
        [SerializeField] private float minXAngle;
        [SerializeField] private float maxXAngle;

        [Inject] private UIDragHandler dragHandler;

        void Start()
        {
            dragHandler.OnDragDeltaAsObservable()
                .Subscribe(Rotate);
        }

        private void Rotate(Vector2 angles)
        {
            angles *= speed;
            yRotateRoot.Rotate(Vector3.up, -angles.x);
            xRotateRoot.Rotate(Vector3.right, angles.y, minXAngle, maxXAngle);
        }
    }
}