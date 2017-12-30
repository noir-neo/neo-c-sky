using UnityEngine;

namespace Gestures.Interface
{
    public interface IDragBehaviour
    {
        int PointerCount { get; }
        void OnDrag(Vector2 delta);
    }
}
