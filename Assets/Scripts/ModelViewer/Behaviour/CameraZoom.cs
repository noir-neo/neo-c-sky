using UnityEngine;

namespace ModelViewer.Behaviour
{
    public sealed class CameraZoom : PinchObserverBase
    {
        [SerializeField] private Camera _camera;
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