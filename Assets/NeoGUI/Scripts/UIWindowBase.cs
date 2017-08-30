using UnityEngine;

namespace NeoGUI
{
    public abstract class UIWindowBase<T> : UIWindowBase
    {
        public virtual void Open(T t)
        {
            base.Open();
        }
    }

    public abstract class UIWindowBase : MonoBehaviour
    {
        public virtual void Open()
        {
            gameObject.SetActive(true);
        }

        public virtual void Suspend()
        {
            gameObject.SetActive(false);
        }

        public virtual void Close()
        {
            Destroy(gameObject);
        }
    }
}

