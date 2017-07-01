using UnityEngine;

namespace ModelViewer
{
    public sealed class CameraZoom : PinchObserverBase
    {
        [SerializeField] Camera _camera;
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