using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Zenject;

namespace NeoC
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        [Inject] private UIDragHandler dragHandler;

        void Start()
        {
            dragHandler.OnDragAsObservable()
                .TakeUntil(dragHandler.OnEndDragAsObservable())
                .RepeatUntilDestroy(this)
                .Select(x => x.delta)
                .Subscribe(x => Move(-x));

            this.OnTriggerEnterAsObservable()
                .Where(collider => collider.gameObject.tag == "Saboten")
                .Subscribe(collider => Attack(collider));
        }

        private void Move(Vector2 move)
        {
            transform.position -= move.X0Y() * 0.1f;
            transform.LookToward(move/*.LimitDirection(8)*/.X0Y());
            _animator.SetFloat("velocity", move.sqrMagnitude);
        }

        private void Attack(Collider collider)
        {
            Saboten saboten;
            if (Saboten(collider, out saboten))
            {
                _animator.SetTrigger("Attack");
                saboten.Break();
            }
        }

        private bool Saboten(Collider collider, out Saboten saboten)
        {
            saboten = collider.GetComponent<Saboten>();
            return saboten != null;
        }
    }
}
