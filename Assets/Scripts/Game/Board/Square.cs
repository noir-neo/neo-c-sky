using System.Collections.Generic;
using System.Diagnostics;
using NeoC.Game.Model;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NeoC.Game.Board
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Renderer))]
    [RequireComponent(typeof(SquarePointerEventHandler))]
    public class Square : MonoBehaviour
    {
        [SerializeField] private SquareModel model;
        public SquareModel Model
        {
            get { return model; }
            set { model = value; }
        }

        [SerializeField] private Renderer _renderer;
        [SerializeField] private SquarePointerEventHandler pointerEventHandler;

        enum SquareColors
        {
            Default,
            Selectable,
            Selected,
            Occupied
        }
        // ToDo: SerializeField (to be a prefab and runtime instancing by master data)
        private static readonly Dictionary<SquareColors, Color32> Colors = new Dictionary<SquareColors, Color32>
        {
            {SquareColors.Default, new Color32(0, 128, 255, 44)},
            {SquareColors.Selectable, new Color32(0, 128, 255, 88)},
            {SquareColors.Selected, new Color32(0, 255, 55, 130)},
            {SquareColors.Occupied, new Color32(255, 0, 32, 130)}
        };

        private Dictionary<EventTriggerType, Subject<SquareModel>> eventSubjects;
        private Dictionary<EventTriggerType, IObservable<SquareModel>> eventAsObservables;

        void Start()
        {
            pointerEventHandler.OnDownAsObservable()
                .Subscribe(_ => Highlight());
            pointerEventHandler.OnExitAsObservable()
                .Subscribe(_ => AllowSelect());
        }

        public Vector2 Position()
        {
            return transform.position.XZ();
        }

        public void Default()
        {
            pointerEventHandler.EnableCollider(false);
            UpdateMaterial(SquareColors.Default);
        }

        public void AllowSelect()
        {
            pointerEventHandler.EnableCollider(true);
            UpdateMaterial(SquareColors.Selectable);
        }

        public void Occupy()
        {
            pointerEventHandler.EnableCollider(false);
            UpdateMaterial(SquareColors.Occupied);
        }

        public void Highlight()
        {
            UpdateMaterial(SquareColors.Selected);
        }

        private void UpdateMaterial(SquareColors color)
        {
            _renderer.material.color = Colors[color];
        }

        public IObservable<SquareModel> OnClickAsObservable()
        {
            return pointerEventHandler.OnClickAsObservable()
                .Select(_ => Model);
        }

        public IObservable<SquareModel> OnDownAsObservable()
        {
            return pointerEventHandler.OnDownAsObservable()
                .Select(_ => Model);
        }

        [Conditional("UNITY_EDITOR")]
        void OnEnable()
        {
            _renderer = GetComponent<Renderer>();
            pointerEventHandler = GetComponent<SquarePointerEventHandler>();
        }

        [Conditional("UNITY_EDITOR")]
        public void UpdateCoordinate(int x, int y)
        {
            model = new SquareModel(x, y);
            gameObject.name = $"{x:D2}_{y:D2}";
        }
    }
}
