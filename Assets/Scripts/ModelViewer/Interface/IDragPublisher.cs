using ModelViewer.EventData;
using UniRx;

namespace ModelViewer.Interface
{
    public interface IDragPublisher
    {
        IObservable<DragEventData> OnDragsAsObservable();
    }
}