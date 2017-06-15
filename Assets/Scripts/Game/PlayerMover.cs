using UnityEngine;

namespace NeoC.Game
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float speed;

        public void Move(Vector2 move)
        {
            move *= speed;
            Move(move.X0Y());
        }

        public void Move(Vector3 move)
        {
            _rigidbody.position += move;
            _rigidbody.LookToward(-move);
            _animator.SetFloat("velocity", move.sqrMagnitude);
        }
    }
}