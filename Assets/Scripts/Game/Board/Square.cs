using System;
using System.Diagnostics;
using UnityEngine;

namespace NeoC.Game.Board
{
    public class Square : MonoBehaviour
    {
        [Serializable]
        public struct BoardCoordinate
        {
            public int x;
            public int y;

            public void SetValue(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [SerializeField] private BoardCoordinate coordinate;

        [Conditional("UNITY_EDITOR")]
        public void UpdateCoordinate(int x, int y)
        {
            coordinate.SetValue(x, y);
            gameObject.name = string.Format("{0:D2}_{1:D2}", x, y);
        }
    }
}
