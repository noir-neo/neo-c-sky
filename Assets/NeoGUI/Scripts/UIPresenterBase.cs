using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NeoGUI
{
    public abstract class UIPresenterBase : MonoBehaviour
    {
        [SerializeField] private UIWindowBase[] windowPrefabs;
        private readonly IList<UIWindowBase> cachedWindows = new List<UIWindowBase>();

        public T Open<T, U>(U arg) where T : UIWindowBase<U>
        {
            var window = GetFromCacheOrCreate<T>();
            window.Open(arg);
            return window;
        }

        public T Open<T>() where T : UIWindowBase
        {
            var window = GetFromCacheOrCreate<T>();
            window.Open();
            return window;
        }

        private T GetFromCacheOrCreate<T>() where T : UIWindowBase
        {
            T cachedWindow;
            if (TryGet(cachedWindows, out cachedWindow))
            {
                return cachedWindow;
            }

            T prefab;
            if (!TryGet(windowPrefabs, out prefab)) return null;
            var window = Instantiate(prefab, transform);
            cachedWindows.Add(window);
            return window;
        }

        public void Suspend<T>() where T : UIWindowBase
        {
            T window;
            if (TryGet(cachedWindows, out window))
            {
                window.Suspend();
            }
        }

        public void Close<T>() where T : UIWindowBase
        {
            T window;
            if (TryGet(cachedWindows, out window))
            {
                cachedWindows.Remove(window);
                window.Close();
            }
        }

        private static bool TryGet<T>(IEnumerable<UIWindowBase> windows, out T window)
            where T : UIWindowBase
        {
            window = windows.FirstOrDefault(w => w is T) as T;
            return window != null;
        }
    }
}
