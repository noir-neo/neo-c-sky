using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using UnityEngine.EventSystems;

public class UIDragHandler : MonoBehaviour
{
    [SerializeField] private Button button;

    public IObservable<PointerEventData> OnBeginDragAsObservable()
    {
        return button.OnBeginDragAsObservable();
    }

    public IObservable<PointerEventData> OnDragAsObservable()
    {
        return button.OnDragAsObservable();
    }

    public IObservable<PointerEventData> OnEndDragAsObservable()
    {
        return button.OnEndDragAsObservable();
    }
}
