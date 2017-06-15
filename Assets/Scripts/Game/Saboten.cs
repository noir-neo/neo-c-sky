using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Diagnostics;

namespace NeoC.Game
{
    public class Saboten : MonoBehaviour
    {
        [SerializeField] private Collider triggerCollider;
        [SerializeField] private List<Rigidbody> childrenRigidbodies;

        public void Break()
        {
            triggerCollider.enabled = false;
            EnableKinematic(childrenRigidbodies, false);
            Blow(childrenRigidbodies);
        }

        private void EnableKinematic(List<Rigidbody> rigidbodies, bool enable = true)
        {
            foreach (var rigidbody in rigidbodies)
            {
                rigidbody.isKinematic = enable;
            }
        }

        private void Blow(List<Rigidbody> rigidbodies)
        {
            foreach (var rigidbody in rigidbodies)
            {
                var force = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
                rigidbody.AddForce(force, ForceMode.Impulse);
            }
        }

        [Conditional("UNITY_EDITOR")]
        public void SerializeChildrenRigidbodies()
        {
            childrenRigidbodies = GetComponentsInChildren<Rigidbody>().ToList();
        }
    }
}
