using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gestures.EventData
{
    public struct DragEventData
    {
        public int PointerCount { get; }
        public Vector2 Delta { get; }

        private DragEventData(int pointerCount, Vector2 delta)
        {
            PointerCount = pointerCount;
            Delta = delta;
        }

        public DragEventData(IEnumerable<Vector2> deltas) :
            this(deltas.Count(),
                deltas.Aggregate((n, next) => Vector2.Lerp(n, next, 0.5f)))
        {
        }
    }
}