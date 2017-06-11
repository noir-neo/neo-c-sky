using UnityEngine;

namespace NeoC.Game
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float speed;

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
    }
}