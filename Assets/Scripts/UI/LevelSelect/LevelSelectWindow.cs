using System.Linq;
using NeoGUI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace NeoC.UI
{
    public class LevelSelectWindow : UIWindowBase
    {
        [SerializeField] private Button[] buttons;

        void Start()
        {
            // FIXME: このクラス UI なのか presenter よくわからん
            OnLevelSelected()
                .StartWith(0)
                .DistinctUntilChanged()
                .SelectMany(x => LevelLoader.LoadLevelAsObservable(x)
                    .Select(_ => x))
                .Pairwise()
                .SelectMany(x => LevelLoader.UnloadLevelAsObservable(x.Previous))
                .Subscribe()
                .AddTo(this);
        }

        private IObservable<int> OnLevelSelected()
        {
            return buttons.Select((b, i) => b.OnClickAsObservable()
                    .Select(_ => i))
                .Merge();
        }
    }
}
