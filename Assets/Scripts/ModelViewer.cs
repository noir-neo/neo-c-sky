using UnityEngine;
using UniRx;
using Zenject;

public class ModelViewer : MonoBehaviour
{
    [SerializeField] Transform xRotateRoot;
    [SerializeField] Transform yRotateRoot;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float maxAngle;
    [SerializeField] private float minAngle;

    [Inject] private UIDragHandler dragHandler;

    void Start()
    {
        dragHandler.OnDragAsObservable()
            .TakeUntil(dragHandler.OnEndDragAsObservable())
            .Select(x => x.delta)
            .RepeatUntilDestroy(this)
            .Subscribe(x => Rotate(x));
    }

    private void Rotate(Vector2 angles)
    {
        angles *= rotationSpeed;
        yRotateRoot.Rotate(Vector3.up, -angles.x);
        xRotateRoot.Rotate(Vector3.right, angles.y, minAngle, maxAngle);
    }
}
