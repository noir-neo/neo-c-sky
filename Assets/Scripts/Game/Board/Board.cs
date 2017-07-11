﻿using System.Collections.Generic;
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

        public IObservable<SquareModel> OnClickSquaresAsObservable()
        {
            return squares.Select(s => s.OnClickAsObservable()).Merge();
        }

        public IObservable<SquareModel> OnDownSquaresAsObservable()
        {
            return squares.Select(s => s.OnDownAsObservable()).Merge();
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
            square = GetSquare(model);
            return square != null;
        }

        public Square GetSquare(SquareModel model)
        {
            return squares.SingleOrDefault(s => s.Model == model);
        }

        public void UpdateSelectables(IEnumerable<SquareModel> selectables)
        {
            var selectableSquares = squares.Where(s => selectables.Contains(s.Model));

            foreach (var square in squares)
            {
                square.AllowSelect(selectableSquares.Contains(square));
            }
        }

        public void UpdateSelectables(bool selectableAll = false)
        {
            foreach (var square in squares)
            {
                square.AllowSelect(selectableAll);
            }
        }

        public void Highlight(SquareModel model)
        {
            Square square;
            if (TryGetSquare(model, out square))
            {
                square.Highlight();
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
