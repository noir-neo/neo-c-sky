using UnityEngine;

namespace ModelViewer.Handler
{
    public interface IDragHandler
    {
        void OnDrag(Vector2 delta);
    }
}