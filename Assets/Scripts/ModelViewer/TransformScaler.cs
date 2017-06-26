using ModelViewer.Handler;
using UnityEngine;

namespace ModelViewer
{
    public class TransformScaler : MonoBehaviour, IPinchHandler
    {
        [SerializeField] Transform scaleRoot;
        [SerializeField] private float speed;
        [SerializeField] private float minScale;
        [SerializeField] private float maxScale;

        public void OnPinch(float magnitude)
        {
            scaleRoot.Scale(magnitude * speed, minScale, maxScale);
        }
    }
}