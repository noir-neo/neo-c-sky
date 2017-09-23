using UnityEngine;

namespace FixFOVOrientation
{
    [RequireComponent(typeof(Camera)), ExecuteInEditMode]
    public class FixFieldOfViewOrientation : MonoBehaviour
    {
        [SerializeField, HideInInspector] private Camera _camera;
        [Range(1, 179)] public float fieldOfView;

        private bool IsLandscape => _camera.aspect > 1;

        void Update()
        {
            _camera.fieldOfView = FixedFieldOfView(fieldOfView, _camera.aspect, IsLandscape);
        }

        private static float FixedFieldOfView(float fieldOfView, float aspect, bool isLandscape)
        {
            if (isLandscape) return fieldOfView / aspect;
            return fieldOfView;
        }

        void OnValidate()
        {
            _camera = GetComponent<Camera>();
        }
    }
}