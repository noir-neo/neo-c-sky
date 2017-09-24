using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ModestTree;
using NeoC.Game.Model;
using UniRx;
using UnityEngine;

namespace NeoC.Game.Board
{
    [CreateAssetMenu]
    public class BoardResources : ScriptableObject
    {
        public Square squarePrefab;
        public Vector2 squaresInterval;
        public List<SquareState> squareStates;
        public GameObject goalPrefab;

        public SquareState SquareState(SquareState.SquareStates state)
        {
            return squareStates.Single(s => s.State == state);
        }
    }
}
