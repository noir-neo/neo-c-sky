using UniRx;
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
        private readonly Subject<UIWindowBase> onOpenStream = new Subject<UIWindowBase>();
        private readonly Subject<UIWindowBase> onSuspendStream = new Subject<UIWindowBase>();
        private readonly Subject<Unit> onCloseStream = new Subject<Unit>();

        public IObservable<UIWindowBase> OnOpenAsObservable()
        {
            return onOpenStream.AsObservable();
        }

        public IObservable<UIWindowBase> OnSuspendAsObservable()
        {
            return onSuspendStream.AsObservable();
        }

        public IObservable<Unit> OnCloseAsObservable()
        {
            return onCloseStream.AsObservable();
        }

        public virtual void Open()
        {
            gameObject.SetActive(true);
            onOpenStream.OnNext(this);
        }

        public virtual void Suspend()
        {
            gameObject.SetActive(false);
            onSuspendStream.OnNext(this);
        }

        public virtual void Close()
        {
            Destroy(gameObject);
            onCloseStream.OnNext(Unit.Default);
        }
    }
}

