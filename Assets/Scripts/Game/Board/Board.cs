using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NeoC.Game.Model;
using UniRx;
using UnityEngine;

namespace NeoC.Game.Board
{
    [ExecuteInEditMode]
    public class Board : MonoBehaviour
    {
        [SerializeField] private List<Square> squares;

        private IObservable<SquareModel> onClickSquareAsObservable;

        void Start()
        {
            OnClickSquaresAsObservable()
                .Subscribe(x => UnityEngine.Debug.Log(x));
        }

        public IObservable<SquareModel> OnClickSquaresAsObservable()
        {
            if (onClickSquareAsObservable != null)
            {
                return onClickSquareAsObservable;
            }

            onClickSquareAsObservable = squares.Select(s => s.OnClickAsObservable()).Merge();
            return onClickSquareAsObservable;
        }

        public bool TryGetSquarePosition(SquareModel model, out Vector2 position)
        {
            Square square;
            if (TryGetSquare(model, out square))
            {
                position = square.Position();
                return true;
            }
            position = Vector2.zero;
            return false;
        }

        public bool TryGetSquare(SquareModel model, out Square square)
        {
            square = squares.SingleOrDefault(s => s.Model == model);
            return square != null;
        }

        public void UpdateSelectable(IEnumerable<SquareModel> selectable)
        {
            var selectableSquares = squares.Where(s => selectable.Contains(s.Model));

            foreach (var square in squares)
            {
                square.AllowSelect(selectableSquares.Contains(square));
            }
        }


        [Conditional("UNITY_EDITOR")]
        public void SerializeSquaresInChildren()
        {
            squares = GetComponentsInChildren<Square>().ToList();
        }

        [Conditional("UNITY_EDITOR")]
        public void CalculateBoardsCoordinate()
        {
            foreach (var zList in squares.BindOrderBy(s => s.transform.localPosition.z, s => s.transform.localPosition.x)
                .Select((v, i) => new { v, i }))
            {
                foreach (var square in zList.v.Select((v, i) => new { v, i }))
                {
                    square.v.UpdateCoordinate(square.i, zList.i);
                }
            }
        }


    }
}
