using UniRx;

namespace ModelViewer.Interface
{
    public interface IPinchPublisher
    {
        IObservable<float> OnPinchAsObservable(float ignoreAngleThreshold);
    }
}