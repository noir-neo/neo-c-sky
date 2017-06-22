using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class UIDragHandler : MonoBehaviour
{
    [SerializeField] private Button button;

    public IObservable<PointerEventData> OnBeginDragAsObservable()
    {
        return button.OnBeginDragAsObservable();
    }

    public IObservable<PointerEventData> OnDragAsObservable()
    {
        return button.OnDragAsObservable();
    }

    public IObservable<PointerEventData> OnEndDragAsObservable()
    {
        return button.OnEndDragAsObservable();
    }

    public IObservable<IEnumerable<PointerEventData>> OnDragsAsObservable(int pointerCount)
    {
        return OnDragAsObservable()
            .BatchFrame()
            .Select(x => x.GroupBy(p => p.pointerId)
                .Select(g => g.First()))
            .Where(x => x.Count() == pointerCount)
            .TakeUntil(OnEndDragAsObservable())
            .RepeatUntilDestroy(this);
    }

    public IObservable<IEnumerable<Vector2>> OnDragsVector2AsObservable(int pointerCount)
    {
        return OnDragsAsObservable(pointerCount)
            .Select(x => x.Select(p => p.delta));
    }

    public IObservable<Vector2> OnDragDeltaAsObservable()
    {
        return OnDragsVector2AsObservable(1)
            .Select(x => x.Single());
    }

    public IObservable<Vector2> OnDragsDeltaAsObservable(int pointerCount)
    {
        return OnDragsVector2AsObservable(pointerCount)
            .Select(x => x.Aggregate((n, next) => Vector2.Lerp(n, next, 0.5f)));
    }

    public IObservable<float> OnPinchAsObservable(float ignoreAngleThreshold)
    {
        return OnDragsAsObservable(2)
            .Where(x => IsDeltasAngleBiggerThan(x.First(), x.Last(), ignoreAngleThreshold))
            .Select(x => DeltaMagnitudeDiff(x.First(), x.Last()));
    }

    private static bool IsDeltasAngleBiggerThan(PointerEventData pointerEventZero, PointerEventData pointerEventOne,
        float angle)
    {
        return Vector2.Angle(pointerEventZero.delta, pointerEventOne.delta) > angle;
    }

    private static float DeltaMagnitudeDiff(PointerEventData pointerEventZero, PointerEventData pointerEventOne)
    {
        return DeltaMagnitudeDiff(pointerEventZero.position, pointerEventZero.delta,
            pointerEventOne.position, pointerEventOne.delta);
    }

    private static float DeltaMagnitudeDiff(Vector2 pointerZeroPosition, Vector2 pointerZeroDelta,
        Vector2 pointerOnePosition, Vector2 pointerOneDelta)
    {
        var pointerZeroPrevPosition = pointerZeroPosition - pointerZeroDelta;
        var pointerOnePrevPosition = pointerOnePosition - pointerOneDelta;

        float prevPointerDeltaMagnitude = (pointerZeroPrevPosition - pointerOnePrevPosition).magnitude;
        float pointerDeltaMagnitude = (pointerZeroPosition - pointerOnePosition).magnitude;

        return pointerDeltaMagnitude - prevPointerDeltaMagnitude;
    }
}