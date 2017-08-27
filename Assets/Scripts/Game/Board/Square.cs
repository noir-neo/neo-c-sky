using System.Diagnostics;
using NeoC.Game.Model;
using UniRx;
using UnityEngine;

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

        public Vector2 Position()
        {
            return transform.position.XZ();
        }

        public void UpdateState(bool selectable, Color32 color)
        {
            pointerEventHandler.EnableCollider(selectable);
            _renderer.material.color = color;
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

        public IObservable<SquareModel> OnExitAsObservable()
        {
            return pointerEventHandler.OnExitAsObservable()
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
