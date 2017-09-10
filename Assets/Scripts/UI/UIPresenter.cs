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
        public ResultWindow OpenResult(bool clear)
        {
            var resultWindow = Open<ResultWindow, Tuple<bool, int>>(new Tuple<bool, int>(clear, 0));
        }
    }
}

