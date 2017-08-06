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
        [SerializeField] private GameObject squarePrefab;
        [SerializeField] private Vector2 squaresInterval;
        [SerializeField] private List<Square> squares;
        [SerializeField] private List<SquareState> squareStates;
        [SerializeField] private GameObject goalPrefab;

        void Start()
        {
            foreach (var square in squares)
            {
                square.OnDownAsObservable()
                    .Subscribe(_ => UpdateState(square, SquareState.SquareStates.Selected));
                square.OnExitAsObservable()
                    .Subscribe(_ => UpdateState(square, SquareState.SquareStates.Selectable));
            }
        }

        public void CreateSquares(List<SquareModel> models)
        {
            foreach (var model in models)
            {
                squares.Add(InstanciateSquare(squarePrefab, model, squaresInterval, transform));
            }
        }

        public void CreateGoal(SquareModel model)
        {
            Instantiate(goalPrefab, GetSquarePosition(model), Quaternion.identity);
        }

        public IObservable<SquareModel> OnClickSquaresAsObservable()
        {
            return squares.Select(s => s.OnClickAsObservable()).Merge();
        }

        public IObservable<SquareModel> OnDownSquaresAsObservable()
        {
            return squares.Select(s => s.OnDownAsObservable()).Merge();
        }

        public IObservable<SquareModel> OnExitSquaresAsObservable()
        {
            return squares.Select(s => s.OnExitAsObservable()).Merge();
        }

        public Vector3 GetSquarePosition(SquareModel model)
        {
            var square = GetSquare(model);
            if (square != null)
            {
                return square.Position().X0Y();
            }
            return Vector3.zero;
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

        public void UpdateStates(SquareState.SquareStates state = SquareState.SquareStates.Default)
        {
            UpdateStates(squares, state);
        }

        public void UpdateStates(IEnumerable<Square> squares, SquareState.SquareStates state = SquareState.SquareStates.Default)
        {
            foreach (var square in squares)
            {
                UpdateState(square, state);
            }
        }

        public void UpdateStates(IEnumerable<SquareModel> squareModels, SquareState.SquareStates state)
        {
            foreach (var squareModel in squareModels)
            {
                UpdateState(squareModel, state);
            }
        }

        public void UpdateState(Square square, SquareState.SquareStates state)
        {
            var squareState = squareStates.SingleOrDefault(s => s.State == state);
            if (squareState != null)
            {
                squareState.UpdateState(square);
            }
        }

        public void UpdateState(SquareModel squareModel, SquareState.SquareStates state)
        {
            Square square;
            if (TryGetSquare(squareModel, out square))
            {
                UpdateState(square, state);
            }
        }

        private static Square InstanciateSquare(GameObject prefab, SquareModel model, Vector2 interval, Transform transform)
        {
            var gameObject = Instantiate(prefab, Vector2.Scale(interval, model).X0Y(), Quaternion.identity, transform);
            var square = gameObject.GetComponent<Square>();
            square.Model = model;
            return square;
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
