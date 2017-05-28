using System.Collections.Generic;
using System.Linq;
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

    public IObservable<IEnumerable<PointerEventData>> OnDragsAsObservable(int count)
    {
        return OnDragAsObservable()
            .BatchFrame()
            .Select(x => x.GroupBy(p => p.pointerId)
                .Select(g => g.First()))
            .Where(x => x.Count() == count);
    }
}
