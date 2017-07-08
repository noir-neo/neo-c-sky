using System.Diagnostics;
using NeoC.Game.Model;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NeoC.Game.Board
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Renderer))]
    [RequireComponent(typeof(Collider))]
    public class Square : ObservableTriggerBase, IPointerClickHandler
    {
        [SerializeField] private SquareModel model;
        public SquareModel Model
        {
            get { return model; }
        }

        [SerializeField] private Renderer renderer;
        [SerializeField] private Collider collider;

        // ToDo: SerializeField (to be a prefab and runtime instancing by master data)
        private static readonly Color defaultColor = new Color32(0, 128, 255, 44);
        private static readonly Color selectableColor = new Color32(0, 128, 255, 88);
        private static readonly Color selectingColor = new Color32(0, 128, 255, 130);

        private Subject<SquareModel> onClick;
        private IObservable<SquareModel> onClickAsObservable;

        public Vector2 Position()
        {
            return transform.position.XZ();
        }

        public void AllowSelect(bool selectable)
        {
            collider.enabled = selectable;
            renderer.material.color = selectable ? selectableColor : defaultColor;
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
        void OnEnable()
        {
            renderer = GetComponent<Renderer>();
            collider = GetComponent<Collider>();
        }

        [Conditional("UNITY_EDITOR")]
        public void UpdateCoordinate(int x, int y)
        {
            model = new SquareModel(x, y);
            gameObject.name = string.Format("{0:D2}_{1:D2}", x, y);
        }
    }
}
