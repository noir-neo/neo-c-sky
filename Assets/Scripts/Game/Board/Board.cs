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

        public bool TryGetSquarePosition(SquareModel coordinate, out Vector2 position)
        {
            var square = squares.SingleOrDefault(s => s.Model == coordinate);
            if (square == null)
            {
                position = Vector2.zero;
                return false;
            }
            position = square.Position();
            return true;
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
