using NeoGUI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace NeoC.UI
{
    public class ResultWindow : UIWindowBase<Tuple<bool, int>>
    {
        [SerializeField] private Button nextButton;
        [SerializeField] private Button titleButton;
        [SerializeField] private Button retryButton;

        public override void Open(Tuple<bool, int> clearCount)
        {
            nextButton.gameObject.SetActive(clearCount.Item1);
            base.Open();
        }

        public IObservable<Unit> OnNextAsObservable()
        {
            return nextButton.OnClickAsObservable();
        }

        public IObservable<Unit> OnTitleAsObservable()
        {
            return titleButton.OnClickAsObservable();
        }

        public IObservable<Unit> OnRetryAsObservable()
        {
            return retryButton.OnClickAsObservable();
        }
    }
}

