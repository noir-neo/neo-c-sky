using UnityEngine;

namespace FixFOVOrientation
{
    [RequireComponent(typeof(Camera)), ExecuteInEditMode]
    public class FixFieldOfViewOrientation : MonoBehaviour
    {
        [SerializeField, HideInInspector] private Camera _camera;
        [Range(1, 179)] public float fieldOfView;
        public float orthographicSize;

        void Update()
        {
            if (_camera.orthographic)
            {
                _camera.SetFixedOrthographicSize(orthographicSize);
            }
            else
            {
                _camera.SetFixedFieldOfView(fieldOfView);
            }
        }

        void OnValidate()
        {
            _camera = GetComponent<Camera>();
        }
    }

    public static class CameraExtensions
    {
        public static void SetFixedOrthographicSize(this Camera camera, float orthographicSize)
        {
            camera.orthographicSize = camera.FixedViewSize(orthographicSize);
        }

        public static void SetFixedFieldOfView(this Camera camera, float fieldOfView)
        {
            camera.fieldOfView = camera.FixedViewSize(fieldOfView);
        }

        private static float FixedViewSize(this Camera camera, float value)
        {
            if (camera.IsLandscape()) return value / camera.aspect;
            return value;
        }

        private static bool IsLandscape(this Camera camera)
        {
            return camera.aspect > 1;
        }
    }
}