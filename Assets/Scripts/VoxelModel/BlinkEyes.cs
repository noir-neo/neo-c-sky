using System.Collections;
using UnityEngine;

namespace VoxelModel
{
    [RequireComponent(typeof(Renderer))]
    public class BlinkEyes : MonoBehaviour
    {
        private Material material;

        void Start()
        {
            material = GetComponent<Renderer>().material;
            StartCoroutine(Blink());
        }

        private IEnumerator Blink()
        {
            yield return new WaitForSeconds(3.0f);
            material.SetFloat("_Ratio", 1.0f);
            yield return new WaitForSeconds(0.1f);
            material.SetFloat("_Ratio", 0.0f);
            yield return Blink();
        }
    }
}
