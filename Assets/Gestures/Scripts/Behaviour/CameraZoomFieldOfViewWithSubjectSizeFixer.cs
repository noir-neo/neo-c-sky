using CameraSubjectSize;
using Gestures.Interface;
using UnityEngine;

namespace Gestures.Behaviour
{
    public sealed class CameraZoomFieldOfViewWithSubjectSizeFixer : MonoBehaviour, IPinchBehaviour
    {
        [SerializeField] private CameraSubjectSizeFixer _camera;
        [SerializeField] private float speed;
        [SerializeField] private float minFieldOfView;
        [SerializeField] private float maxFieldOfView;

        void IPinchBehaviour.OnPinch(float magnitude)
        {
            float fieldOfView = _camera.fieldOfView;
            fieldOfView += magnitude * -speed;
            _camera.fieldOfView = Mathf.Clamp(fieldOfView, minFieldOfView, maxFieldOfView);
        }
    }
}