using UnityEngine;

namespace VoxelModel
{
    [ExecuteInEditMode]
    public class PivotRotator : MonoBehaviour
    {
        private static readonly float gizmoSize = 0.1f;
        private static readonly Color gizmoColor = Color.yellow;

        [SerializeField] private Vector3 point;
        public Vector3 rotation;

        private Vector3 WorldPivot()
        {
            return transform.TransformPoint(point);
        }

        private Quaternion WorldRotation()
        {
            return  transform.rotation * Quaternion.Inverse(transform.localRotation) * Quaternion.Euler(rotation);
        }

        void Update()
        {
            transform.SetRotationAround(WorldPivot(), WorldRotation());
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(WorldPivot(), gizmoSize);
        }
    }

    public static class TransformExtensions
    {
        public static void SetRotationAround(this Transform transform, Vector3 point, Quaternion rotation)
        {
            var position = rotation * Quaternion.Inverse(transform.rotation) * (transform.position - point) + point;
            transform.SetPositionAndRotation(position, rotation);
        }
    }
}
