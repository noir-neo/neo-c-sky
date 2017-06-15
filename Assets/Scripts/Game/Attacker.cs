using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace NeoC.Game
{
    public class Attacker : ObservableTriggerBase
    {
        [SerializeField] private Animator _animator;

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