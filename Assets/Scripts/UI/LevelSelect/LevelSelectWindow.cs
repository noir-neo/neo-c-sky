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

        public IObservable<int> OnLevelSelected()
        {
            return buttons.Select((b, i) => b.OnClickAsObservable()
                    .Select(_ => i))
                .Merge();
        }
    }
}
