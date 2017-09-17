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
            OnLevelSelected()
                .DistinctUntilChanged()
                .Subscribe(x => LevelLoader.LoadLevel(x))
                .AddTo(this);
        }

        public override void Open()
        {
            LevelLoader.LoadLevel(0);
        }

        private IObservable<int> OnLevelSelected()
        {
            return buttons.Select((b, i) => b.OnClickAsObservable()
                    .Select(_ => i))
                .Merge();
        }
    }
}
