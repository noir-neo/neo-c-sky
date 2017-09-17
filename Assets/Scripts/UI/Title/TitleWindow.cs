using System;
using NeoGUI;
using UniRx;
using UnityEngine;

namespace NeoC.UI
{
    public class TitleWindow : UIWindowBase
    {
        [SerializeField] private int displayTimeMillseconds;

        void Awake()
        {
            OnOpenAsObservable()
                .Delay(TimeSpan.FromMilliseconds(displayTimeMillseconds))
                .Subscribe(_ => Close())
                .AddTo(this);
            LevelLoader.LoadLevel(0);
        }
    }
}

