using FixFOVOrientation;
using UnityEngine;

namespace ModelViewer.Observer
{
    public sealed class CameraZoomFixFieldOfViewOrientation : PinchObserverBase
    {
        [SerializeField] private FixFieldOfViewOrientation _camera;
        [SerializeField] private float speed;
        [SerializeField] private float minFieldOfView;
        [SerializeField] private float maxFieldOfView;

        protected override void OnPinch(float magnitude)
        {
            float fieldOfView = _camera.fieldOfView;
            fieldOfView += magnitude * -speed;
            _camera.fieldOfView = Mathf.Clamp(fieldOfView, minFieldOfView, maxFieldOfView);
        }
    }
}