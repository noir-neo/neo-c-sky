using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace NeoC.Game
{
    public class PieceMover : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float speed;

        public void MoveAndLookTo(Vector3 targetPos, Quaternion targetRotation)
        {
            MoveTo(targetPos);
            LookRotation(targetRotation);
        }

        public void MoveTo(Vector3 targetPos, Action onCompleted = null)
        {
            MoveTo(targetPos, speed, onCompleted);
        }

        private void MoveTo(Vector3 targetPos, float smooth, Action onCompleted = null)
        {
            var startPos = transform.position;
            float startTime = Time.time;
            float journeyLength = Vector3.Distance(startPos, targetPos);

            LookAt(targetPos);
            this.UpdateAsObservable()
                .RepeatUntilDestroy(gameObject)
                .TakeWhile(_ => Vector3.Distance(transform.position, targetPos) > 0.01f)
                .Subscribe(
                    _ => transform.UpdatePositionLerp(startTime, smooth, journeyLength, startPos, targetPos),
                    () =>
                    {
                        onCompleted?.Invoke();
                    });
        }

        public void PositionTo(Vector3 targetPos)
        {
            transform.position = targetPos;
        }

        public void LookAt(Vector2 target)
        {
            LookAt(target.X0Y());
        }

        public void LookAt(Vector3 target)
        {
            transform.LookAt(target);
        }

        public void LookRotation(Quaternion targetRotation, Action onCompleted = null)
        {
            LookRotation(targetRotation, speed * 100, onCompleted);
        }

        private void LookRotation(Quaternion targetRotation, float smooth, Action onCompleted = null)
        {
            var startRotation = transform.rotation;
            float startTime = Time.time;
            float journeyAngle = Quaternion.Angle(startRotation, targetRotation);

            this.UpdateAsObservable()
                .RepeatUntilDestroy(gameObject)
                .TakeWhile(_ => Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
                .Subscribe(
                    _ => transform.UpdateRotationLerp(startTime, smooth, journeyAngle, startRotation, targetRotation),
                    () =>
                    {
                        onCompleted?.Invoke();
                    });
        }

        public void Kill()
        {
            gameObject.SetActive(false);
        }
    }
}