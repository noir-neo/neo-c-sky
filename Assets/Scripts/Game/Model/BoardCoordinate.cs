using System;
using UnityEngine;

namespace NeoC.Game.Model
{
    [Serializable]
    public struct BoardCoordinate : IEquatable<BoardCoordinate>
    {
        [SerializeField] private int x;
        [SerializeField] private int y;
        public int X {
            get { return x; }
            private set { x = value; }
        }
        public int Y {
            get { return y; }
            private set { y = value; }
        }

        public void SetValue(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj is BoardCoordinate)
            {
                return Equals((BoardCoordinate)obj);
            }
            return false;
        }

        public bool Equals(BoardCoordinate p)
        {
            return X == p.X && Y == p.Y;
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }

        public static bool operator ==(BoardCoordinate lhs, BoardCoordinate rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(BoardCoordinate lhs, BoardCoordinate rhs)
        {
            return !lhs.Equals(rhs);
        }
    }
}