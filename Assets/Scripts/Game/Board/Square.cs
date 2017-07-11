using System.Collections.Generic;
using System.Diagnostics;
using NeoC.Game.Model;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

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
        }

        [SerializeField] private Renderer _renderer;
        [SerializeField] private SquarePointerEventHandler pointerEventHandler;

        enum SquareColors
        {
            Default,
            Selectable,
            Selecting
        }
        // ToDo: SerializeField (to be a prefab and runtime instancing by master data)
        private static readonly Dictionary<SquareColors, Color32> Colors = new Dictionary<SquareColors, Color32>
        {
            {SquareColors.Default, new Color32(0, 128, 255, 44)},
            {SquareColors.Selectable, new Color32(0, 128, 255, 88)},
            {SquareColors.Selecting, new Color32(0, 255, 55, 130)}
        };

        private Dictionary<EventTriggerType, Subject<SquareModel>> eventSubjects;
        private Dictionary<EventTriggerType, IObservable<SquareModel>> eventAsObservables;

        void Start()
        {
            pointerEventHandler.OnDownAsObservable()
                .Subscribe(_ => UpdateMaterial(SquareColors.Selecting));
            pointerEventHandler.OnExitAsObservable()
                .Subscribe(_ => UpdateMaterial(SquareColors.Selectable));
        }

        public Vector2 Position()
        {
            return transform.position.XZ();
        }

        public void AllowSelect(bool selectable)
        {
            pointerEventHandler.EnableCollider(selectable);
            UpdateMaterial(selectable ? SquareColors.Selectable : SquareColors.Default);
        }

        public void Highlight()
        {
            UpdateMaterial(SquareColors.Selecting);
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
            gameObject.name = string.Format("{0:D2}_{1:D2}", x, y);
        }
    }
}
