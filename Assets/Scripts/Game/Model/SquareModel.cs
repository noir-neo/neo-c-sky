using System;
using NeoC.Game.Board;
using UnityEngine;

namespace NeoC.Game.Model
{
    [Serializable]
    public struct SquareModel : IEquatable<SquareModel>
    {
        [SerializeField] private int x;
        [SerializeField] private int y;
        public int X {
            get { return x; }
        }
        public int Y {
            get { return y; }
        }

        public SquareModel(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj is SquareModel)
            {
                return Equals((SquareModel)obj);
            }
            return false;
        }

        public bool Equals(SquareModel p)
        {
            return X == p.X && Y == p.Y;
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }

        public static bool operator ==(SquareModel lhs, SquareModel rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(SquareModel lhs, SquareModel rhs)
        {
            return !lhs.Equals(rhs);
        }

        public static SquareModel operator +(SquareModel lhs, SquareModel rhs)
        {
            return new SquareModel(lhs.X + rhs.X, lhs.Y + rhs.Y);
        }

        public static SquareModel operator -(SquareModel lhs, SquareModel rhs)
        {
            return new SquareModel(lhs.X - rhs.X, lhs.Y - rhs.Y);
        }

        public static implicit operator Vector2(SquareModel val)
        {
            return new Vector2(val.X, val.Y);
        }
    }
}