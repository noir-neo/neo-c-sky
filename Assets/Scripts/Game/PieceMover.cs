using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace NeoC.Game
{
    public class PieceMover : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float speed;

        public IObservable<Unit> MoveToAsObservable(Vector3 targetPos)
        {
            return MoveToAsObservable(targetPos, speed);
        }

        private IObservable<Unit> MoveToAsObservable(Vector3 targetPos, float smooth)
        {
            var startPos = transform.position;
            float startTime = Time.time;

            LookAt(targetPos);

            return this.UpdateAsObservable()
                .TakeUntilDestroy(gameObject)
                .Do(_ => transform.UpdatePositionLerp(startTime, smooth, startPos, targetPos))
                .First(_ => Vector3.Distance(transform.position, targetPos) < 0.01f);
        }

        public IObservable<Unit> LookRotationAsObservable(Quaternion targetRotation)
        {
            return LookRotationAsObservable(targetRotation, speed * 100);
        }

        private IObservable<Unit> LookRotationAsObservable(Quaternion targetRotation, float smooth)
        {
            var startRotation = transform.rotation;
            float startTime = Time.time;
            float journeyAngle = Quaternion.Angle(startRotation, targetRotation);

            return this.UpdateAsObservable()
                .TakeUntilDestroy(this)
                .Do(_ => transform.UpdateRotationLerp(startTime, smooth, journeyAngle, startRotation, targetRotation))
                .First(_ => Quaternion.Angle(transform.rotation, targetRotation) < 0.01f);
        }

        public void PositionTo(Vector3 targetPos)
        {
            transform.position = targetPos;
        }

        public void LookAt(Vector3 target)
        {
            transform.LookAt(target);
        }

        public void Kill()
        {
            gameObject.SetActive(false);
        }
    }
}