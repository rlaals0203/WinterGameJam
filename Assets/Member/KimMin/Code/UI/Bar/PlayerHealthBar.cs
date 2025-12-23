using System;
using KimMin.Core;
using KimMin.Events;
using UnityEngine;

namespace KimMin.UI.Bar
{
    public class PlayerHealthBar : MonoBehaviour
    {
        [SerializeField] private BarComponent bar;

        private void Awake()
        {
            GameEventBus.AddListener<PlayerHealthEvent>(HandleHealthChange);
        }

        private void OnDestroy()
        {
            GameEventBus.RemoveListener<PlayerHealthEvent>(HandleHealthChange);
        }

        private void HandleHealthChange(PlayerHealthEvent evt)
        {
            bar.SetSlider(evt.health, evt.maxHealth);
        }
    }
}