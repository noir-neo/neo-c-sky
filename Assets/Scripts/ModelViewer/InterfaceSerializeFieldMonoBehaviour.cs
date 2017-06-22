using UnityEngine;

namespace ModelViewer
{
    public class InterfaceSerializeFieldMonoBehaviour<T> : MonoBehaviour
        where T : class
    {
        [SerializeField] private MonoBehaviour implementsMonoBehaviour;
        private T _interface;
        protected T Interface
        {
            get
            {
                if (_interface == default(T))
                {
                    _interface = implementsMonoBehaviour as T;
                }
                return _interface;
            }
        }

        protected void OnValidate()
        {
            var t = implementsMonoBehaviour as T;
            if (t == default(T))
            {
                implementsMonoBehaviour = null;
            }
        }
    }
}