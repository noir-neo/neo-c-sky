using Gestures.Interface;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class ScrollHandler : MonoBehaviour,  IPinchPublisher
{
    public IObservable<float> OnPinchAsObservable()
    {
        return this.UpdateAsObservable()
            .TakeUntilDestroy(this)
            .Select(_ => Input.mouseScrollDelta.y)
            .DistinctUntilChanged();
    }
}
