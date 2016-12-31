using UnityEngine;
using UniRx;

namespace NeoC
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        void Start()
        {
            this.OnSwipeAsObservable()
                .Subscribe(x => Move(x));
        }

        private void Move(Vector2 move)
        {
            transform.position -= move.X0Y() * 0.1f;
            transform.LookToward(move/*.LimitDirection(8)*/.X0Y());
            _animator.SetFloat("velocity", move.sqrMagnitude);
        }
    }
}
