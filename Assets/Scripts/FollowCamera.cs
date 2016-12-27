using NeoC;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smooth;

    void Start()
    {
        StartFollow(transform, target, Time.deltaTime * smooth);
    }

    private void StartFollow(Transform transform, Transform target, float t)
    {
        if (transform == null || target == null)
        {
            return;
        }

        var offset = transform.position - target.position;
        target.OnPositionChangeAsObservable()
            .SelectMany(this.LateUpdateAsObservable())
            .Subscribe(_ =>
                transform.PositionLerp(target.position + offset, t));
    }
}
