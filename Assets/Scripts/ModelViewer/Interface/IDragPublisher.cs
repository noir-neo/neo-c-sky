using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace ModelViewer.Interface
{
    public interface IDragPublisher
    {
        IObservable<IEnumerable<Vector2>> OnDragsAsObservable();
    }
}