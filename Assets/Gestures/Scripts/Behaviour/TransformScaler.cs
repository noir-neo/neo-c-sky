using Gestures.Interface;
using UnityEngine;

namespace Gestures.Behaviour
{
    public sealed class TransformScaler : MonoBehaviour, IPinchBehaviour
    {
        [SerializeField] Transform scaleRoot;
        [SerializeField] private float speed;
        [SerializeField] private float minScale;
        [SerializeField] private float maxScale;

        void IPinchBehaviour.OnPinch(float magnitude)
        {
            scaleRoot.Scale(magnitude * speed, minScale, maxScale);
        }
    }
}