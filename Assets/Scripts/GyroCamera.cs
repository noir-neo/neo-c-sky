using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class GyroCamera : MonoBehaviour
{

    void Start()
    {
        Input.gyro.enabled = true;
        this.UpdateAsObservable()
            .Select(_ => Input.gyro.attitude)
            .DistinctUntilChanged()
            .RepeatUntilDestroy(gameObject)
            .Select(x => Quaternion.Inverse(x))
            .Select(x => x * Quaternion.Euler(90f, 0, 0))
            .Subscribe(x => transform.rotation = x);
    }
}
