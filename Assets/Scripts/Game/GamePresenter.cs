﻿using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

namespace NeoC.Game
{
    public class GamePresenter : MonoBehaviour
    {
        [Inject] private UIDragHandler dragHandler;
        [Inject] private PlayerController playerController;

        void Start()
        {
            dragHandler.OnDragsAsObservable(1)
                .TakeUntil(dragHandler.OnEndDragAsObservable())
                .RepeatUntilDestroy(this)
                .Select(x => x.Single().delta)
                .Subscribe(playerController.Move);

            playerController.OnAttackAsObservable()
                .Subscribe(saboten => saboten.Break());
        }
    }
}