using UnityEngine;
using UniRx;

namespace NeoC
{
    public class PlayerController : MonoBehaviour
    {

        void Start()
        {
            this.OnSwipeAsObservable()
                .Subscribe(x => Move(x));
        }

        private void Move(Vector3 move)
        {
            move = move.X0Y() * 0.1f;

            transform.position -= move;
            transform.LookToward(move);
        }
    }
}
