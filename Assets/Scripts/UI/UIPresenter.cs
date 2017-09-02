using NeoGUI;
using UnityEngine;
using UniRx;

namespace NeoC.UI
{
    public class UIPresenter : UIPresenterBase
    {
        void Start()
        {
            var titleWindow = Open<TitleWindow>();
            titleWindow.OnCloseAsObservable()
                .Subscribe(_ => Open<LevelSelectWindow>());
        }
    }
}

