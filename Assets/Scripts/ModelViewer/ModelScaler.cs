using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using UnityEngine.EventSystems;
using Zenject;

namespace NeoC.ModelViewer
{
    public class ModelScaler : MonoBehaviour
    {
        [SerializeField] Transform scaleRoot;
        [SerializeField] private float speed;
        [SerializeField] private float minScale;
        [SerializeField] private float maxScale;
        [SerializeField] private float ignoreAngleThreshold;

        [Inject] private UIDragHandler dragHandler;

        void Start()
        {
            dragHandler.OnDragsAsObservable(2)
                .TakeUntil(dragHandler.OnEndDragAsObservable())
                .RepeatUntilDestroy(this)
                .Where(x => IsDeltasAngleBiggerThan(x.First(), x.Last(), ignoreAngleThreshold))
                .Select(x => DeltaMagnitudeDiff(x.First(), x.Last()))
                .Subscribe(Scale);
        }

        private static bool IsDeltasAngleBiggerThan(PointerEventData pointerEventZero, PointerEventData pointerEventOne, float angle)
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

        private void Scale(float magnitude)
        {
            scaleRoot.Scale(magnitude * speed, minScale, maxScale);
        }
    }
}