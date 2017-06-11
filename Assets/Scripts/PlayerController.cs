using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace NeoC
{
    public class PlayerController : ObservableTriggerBase
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float speed;

        private Subject<Saboten> onAttack;

        void Start()
        {
            this.OnTriggerEnterAsObservable()
                .Where(collider => collider.gameObject.tag == "Saboten")
                .Select(collider => collider.GetComponent<Saboten>())
                .Where(saboten => saboten != null)
                .Subscribe(Attack);

            OnAttackAsObservable()
                .Subscribe(_ => TriggerAnimator("Attack"));
        }

        public void Move(Vector2 move)
        {
            move *= speed;
            Move(move.X0Y());
        }

        public void Move(Vector3 move)
        {
            transform.position += move;
            transform.LookToward(-move);
            _animator.SetFloat("velocity", move.sqrMagnitude);
        }

        private void TriggerAnimator(string name)
        {
            if (_animator == null) return;
            _animator.SetTrigger(name);
        }

        private void Attack(Saboten saboten)
        {
            if (onAttack == null) return;
            onAttack.OnNext(saboten);
        }

        public IObservable<Saboten> OnAttackAsObservable()
        {
            return onAttack ?? (onAttack = new Subject<Saboten>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onAttack != null)
            {
                onAttack.OnCompleted();
            }
        }
    }
}