using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace NeoC
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        void Start()
        {
            this.OnTriggerEnterAsObservable()
                .Where(collider => collider.gameObject.tag == "Saboten")
                .Subscribe(collider => Attack(collider));
        }

        public void Move(Vector2 move)
        {
            Move(move.X0Y());
        }

        public void Move(Vector3 move)
        {
            transform.position += move;
            transform.LookToward(-move);
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
