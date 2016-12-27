using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public static class ObservableExtensions
{

    public static IObservable<Vector3> OnSwipeAsObservable(this Component component)
    {
        if (component == null || component.gameObject == null) return Observable.Empty<Vector3>();
        return component.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))
            .SelectMany(component.UpdateAsObservable())
            .TakeUntil(component.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonUp(0)))
            .Select(_ => Input.mousePosition)
            .DistinctUntilChanged()
            .Buffer(2, 1)
            .RepeatUntilDestroy(component)
            .Where(x => x.Count == 2)
            .Select(x => x.First() - x.Last());
    }

    public static IObservable<Vector3> OnPositionChangeAsObservable(this Transform target)
    {
        return target.UpdateAsObservable()
            .Select(_ => target.position)
            .DistinctUntilChanged();
    }
}
