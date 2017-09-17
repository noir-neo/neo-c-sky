using NeoGUI;
using UnityEngine;
using UniRx;

namespace NeoC.UI
{
    public class UIPresenter : UIPresenterBase
    {
        void Start()
        {
            OpenTitle();
        }

        public TitleWindow OpenTitle()
        {
            var titleWindow = Open<TitleWindow>();
            titleWindow.OnCloseAsObservable()
                .Subscribe(_ => Open<LevelSelectWindow>());
            return titleWindow;
        }

        public ResultWindow OpenResult(bool clear)
        {
            var resultWindow = Open<ResultWindow, Tuple<bool, int>>(new Tuple<bool, int>(clear, 0));
            resultWindow.OnTitleAsObservable()
                .Subscribe(_ =>
                {
                    Close<ResultWindow>();
                    OpenTitle();
                })
                .AddTo(this);
            return resultWindow;
        }
    }
}

