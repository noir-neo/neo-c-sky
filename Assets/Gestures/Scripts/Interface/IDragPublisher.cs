using Gestures.EventData;
using UniRx;

namespace Gestures.Interface
{
    public interface IDragPublisher
    {
        IObservable<DragEventData> OnDragsAsObservable();
    }
}