using NeoC.Game;
using UnityEngine;

namespace NeoC
{
    public class SabotenGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject sabotenPrefab;

        public Saboten Generate()
        {
            var gameObject = Instantiate(sabotenPrefab);
            return gameObject.GetComponent<Saboten>();
        }

        public Saboten Generate(Vector3 position)
        {
            var saboten = Generate();
            saboten.transform.position = position;
            return saboten;
        }
    }
}