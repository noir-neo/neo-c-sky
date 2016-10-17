using UnityEngine;
using System.Linq;
using UniRx;
using UniRx.Triggers;

public class ModelViewer : MonoBehaviour
{
    [SerializeField] Transform xRotateRoot;
    [SerializeField] Transform yRotateRoot;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float maxAngle;
    [SerializeField] private float minAngle;

    void Start()
    {
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))
            .SelectMany(this.UpdateAsObservable())
            .TakeUntil(this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonUp(0)))
            .Select(_ => Input.mousePosition)
            .DistinctUntilChanged()
            .Buffer(2, 1)
            .RepeatUntilDestroy(this)
            .Where(x => x.Count == 2)
            .Select(x => x.First() - x.Last())
            .Subscribe(x => Rotate(x));
    }

    private void Rotate(Vector2 angles)
    {
        angles *= rotationSpeed;
        yRotateRoot.Rotate(Vector3.up, angles.x);
        xRotateRoot.Rotate(Vector3.right, -angles.y, minAngle, maxAngle);
    }
}
