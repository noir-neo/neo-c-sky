using UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace ModelViewer
{
    public class ModelScaler : MonoBehaviour
    {
        [SerializeField] Transform scaleRoot;
        [SerializeField] private float speed;
        [SerializeField] private float minScale;
        [SerializeField] private float maxScale;
        [SerializeField] private float ignoreAngleThreshold;

        [Inject] private UIDragHandler dragHandler;

        void Start()
        {
            dragHandler.OnPinchAsObservable(ignoreAngleThreshold)
                .Subscribe(Scale);
        }

        private void Scale(float magnitude)
        {
            scaleRoot.Scale(magnitude * speed, minScale, maxScale);
        }
    }
}