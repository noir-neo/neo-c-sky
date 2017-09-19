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
                .Subscribe(_ => OpenLevelSelect());
            LevelLoader.LoadLevel(0);
            return titleWindow;
        }

        public LevelSelectWindow OpenLevelSelect()
        {
            var levelSelectWindow = Open<LevelSelectWindow>();
            levelSelectWindow.OnLevelSelected()
                .Subscribe(x => LevelLoader.LoadLevel(x))
                .AddTo(this);
            return levelSelectWindow;
        }

        public ResultWindow OpenResult(bool clear)
        {
            bool existsNextLevel = LevelLoader.ExistsNextLevel();
            var resultWindow = Open<ResultWindow, Tuple<bool, int>>(new Tuple<bool, int>(clear && existsNextLevel, 0));
            resultWindow.OnTitleAsObservable()
                .Subscribe(_ =>
                {
                    Close<ResultWindow>();
                    OpenTitle();
                })
                .AddTo(this);
            resultWindow.OnRetryAsObservable()
                .Subscribe(_ =>
                {
                    Close<ResultWindow>();
                    LevelLoader.ReloadLevel();
                });
            resultWindow.OnNextAsObservable()
                .Subscribe(_ =>
                {
                    Close<ResultWindow>();
                    LevelLoader.LoadNextLevel();
                })
                .AddTo(this);
            return resultWindow;
        }
    }
}

