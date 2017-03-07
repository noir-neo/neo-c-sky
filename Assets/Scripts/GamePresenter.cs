using NeoC;
using UniRx;
using UnityEngine;
using Zenject;

public class GamePresenter : MonoBehaviour
{

    [Inject] private UIDragHandler dragHandler;
    [Inject] private PlayerController playerController;

    void Start()
    {
        // FIXME: Fat presenter
        dragHandler.OnDragAsObservable()
            .TakeUntil(dragHandler.OnEndDragAsObservable())
            .RepeatUntilDestroy(this)
            .Select(x => x.delta)
            .Subscribe(x => playerController.Move(x * 0.1f));
    }
}
