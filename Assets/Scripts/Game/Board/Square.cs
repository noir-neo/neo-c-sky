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
        [SerializeField] private SquareModel model;
        public SquareModel Model
        {
            get { return model; }
        }

        [SerializeField] private Renderer renderer;
        [SerializeField] private Collider collider;

        private Subject<SquareModel> onClick;
        private IObservable<SquareModel> onClickAsObservable;

        public Vector2 Position()
        {
            return transform.position.XZ();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (onClick != null)
            {
                onClick.OnNext(model);
            }
        }

        public IObservable<SquareModel> OnClickAsObservable()
        {
            if (onClickAsObservable != null)
            {
                return onClickAsObservable;
            }

            onClick = new Subject<SquareModel>();
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
            model = new SquareModel(x, y);
            gameObject.name = string.Format("{0:D2}_{1:D2}", x, y);
        }
    }
}
