using UnityEngine;

namespace ModelViewer.Handler
{
    interface IDragsHandler
    {
        void OnDrag(Vector2 delta);
    }
}