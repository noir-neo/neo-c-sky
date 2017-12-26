using UnityEngine;

namespace ModelViewer.Interface
{
    public interface IDragBehaviour
    {
        int PointerCount { get; }
        void OnDrag(Vector2 delta);
    }
}
