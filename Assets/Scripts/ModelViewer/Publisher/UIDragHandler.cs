using System.Collections.Generic;
using System.Linq;
using ModelViewer.EventData;
using ModelViewer.Interface;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ModelViewer.Publisher
{
    [RequireComponent(typeof(Button))]
    public class UIDragHandler : MonoBehaviour, IDragPublisher, IPinchPublisher
    {
        [SerializeField] private Button button;

        private IObservable<PointerEventData> OnDragAsObservable()
        {
            return button.OnDragAsObservable();
        }

        private IObservable<IEnumerable<PointerEventData>> OnDragsAsObservableInternal()
        {
            return OnDragAsObservable()
                .TakeUntilDestroy(this)
                .BatchFrame()
                .Select(x => x.GroupBy(p => p.pointerId)
                    .Select(g => g.First()));
        }

        IObservable<DragEventData> IDragPublisher.OnDragsAsObservable()
        {
            return OnDragsAsObservableInternal()
                .Select(x => x.Select(p => p.delta))
                .Select(x => new DragEventData(x));
        }

        IObservable<float> IPinchPublisher.OnPinchAsObservable(float ignoreAngleThreshold)
        {
            return OnDragsAsObservableInternal()
                .Where(x => x.Count() == 2)
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
}