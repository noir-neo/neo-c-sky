using System.Diagnostics;
using NeoC.Game.Model;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NeoC.Game.Board
{
    public class Square : ObservableTriggerBase, IPointerClickHandler
    {
        [SerializeField] private BoardCoordinate coordinate;
        public BoardCoordinate Coordinate
        {
            get { return coordinate; }
        }

        private Subject<BoardCoordinate> onClick;
        private IObservable<BoardCoordinate> onClickAsObservable;

        public Vector2 Position()
        {
            return transform.position.XZ();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (onClick != null)
            {
                onClick.OnNext(coordinate);
            }
        }

        public IObservable<BoardCoordinate> OnClickAsObservable()
        {
            if (onClickAsObservable != null)
            {
                return onClickAsObservable;
            }

            onClick = new Subject<BoardCoordinate>();
            onClickAsObservable = onClick.AsObservable();
            return onClickAsObservable;
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onClick != null)
            {
                onClick.OnCompleted();
            }
        }

        [Conditional("UNITY_EDITOR")]
        public void UpdateCoordinate(int x, int y)
        {
            coordinate.SetValue(x, y);
            gameObject.name = string.Format("{0:D2}_{1:D2}", x, y);
        }
    }
}
