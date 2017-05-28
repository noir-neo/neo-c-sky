using System.Linq;
using UnityEngine;
using UniRx;
using Zenject;

namespace NeoC.ModelViewer
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
            dragHandler.OnDragsAsObservable(1)
                .TakeUntil(dragHandler.OnEndDragAsObservable())
                .RepeatUntilDestroy(this)
                .Select(x => x.Single().delta)
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
