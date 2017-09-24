using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NeoC.Game.Model;
using UniRx;
using UnityEditor;
using UnityEngine;

namespace NeoC.Game.Board
{
    [ExecuteInEditMode]
    public class Board : MonoBehaviour
    {
        [SerializeField] private List<Square> squares;
        [SerializeField] private GameObject goal;
        [SerializeField] private BoardResources resources;

        public void CreateSquares(IEnumerable<SquareModel> models)
        {
            if (squares.Any())
            {
                return;
            }
            squares = models.Select(m => InstantiateSquare(resources.squarePrefab, m, resources.squaresInterval, transform))
                .ToList();
        }

        public void CreateGoal(SquareModel model)
        {
            if (goal != null)
            {
                return;
            }
            goal = Instantiate(resources.goalPrefab, GetSquarePosition(model), Quaternion.identity, transform);
        }

        public PieceMover Instantiate(PieceMover prefab, EnemyModel enemyModel)
        {
            return Instantiate(prefab,
                GetSquarePosition(enemyModel.CurrentSquare.Value),
                enemyModel.CurrentRotation.Value,
                transform);
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

        public void UpdateStates(IEnumerable<SquareModel> squareModels, SquareState.SquareStates state = SquareState.SquareStates.Default)
        {
            foreach (var squareModel in squareModels)
            {
                UpdateState(squareModel, state);
            }
        }

        public void UpdateStatesExcept(IEnumerable<SquareModel> squareModels, SquareState.SquareStates state = SquareState.SquareStates.Default)
        {
            UpdateStates(squares.Except(squareModels.Select(GetSquare)), state);
        }

        public void UpdateStatesExcept(SquareModel squareModel, SquareState.SquareStates state = SquareState.SquareStates.Default)
        {
            UpdateStatesExcept(new [] { squareModel });
        }

        public void UpdateState(Square square, SquareState.SquareStates state)
        {
            resources.SquareState(state).UpdateState(square);
        }

        public void UpdateState(SquareModel squareModel, SquareState.SquareStates state)
        {
            Square square;
            if (TryGetSquare(squareModel, out square))
            {
                UpdateState(square, state);
            }
        }

        private static Square InstantiateSquare(Square prefab, SquareModel model, Vector2 interval, Transform transform)
        {
            var square = Instantiate(prefab, Vector2.Scale(interval, model).X0Y(), Quaternion.identity, transform);
            square.Model = model;
            return square;
        }

        private new static T Instantiate<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent) where T : Component
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                var instance = PrefabUtility.InstantiatePrefab(prefab) as T;
                instance.gameObject.transform.parent = parent;
                instance.gameObject.transform.SetPositionAndRotation(position, rotation);
                return instance;
            }
#endif
            return GameObject.Instantiate(prefab, position, rotation, parent);
        }

        private static GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                var instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                instance.transform.parent = parent;
                instance.transform.SetPositionAndRotation(position, rotation);
                return instance;
            }
#endif
            return GameObject.Instantiate(prefab, position, rotation, parent);
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

        [Conditional("UNITY_EDITOR")]
        public void GenerateBoardFromMaster()
        {
            squares.Clear();
            goal = null;
            foreach (Transform child in transform.Cast<Transform>().ToList())
            {
                DestroyImmediate(child.gameObject);
            }
            SceneManagerUtils.GetComponentInRoot<GamePresenter>().GenerateBoardFromMaster(this);
        }
    }
}
