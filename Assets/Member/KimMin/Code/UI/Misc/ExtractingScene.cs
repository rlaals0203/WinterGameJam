using System;
using EasyTransition;
using UnityEngine;

namespace KimMin.UI.Misc
{
    public class ExtractingScene : MonoBehaviour
    {
        [SerializeField] private TransitionSettings changeEffect;

        private void Start()
        {
            WaitForTransition();
        }

        private async void WaitForTransition()
        {
            await Awaitable.WaitForSecondsAsync(3f);
            TransitionManager.Instance().Transition(SceneName.Ready, changeEffect, 0);
        }
    }
}