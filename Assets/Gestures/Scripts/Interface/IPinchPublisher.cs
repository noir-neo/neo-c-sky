using UniRx;

namespace Gestures.Interface
{
    public interface IPinchPublisher
    {
        IObservable<float> OnPinchAsObservable();
    }
}