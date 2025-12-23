using System;
using System.Linq;
using Code.Core;
using KimMin.Core;
using KimMin.Dependencies;
using KimMin.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Entities
{
    public class PlayerInkCompo : MonoBehaviour, IEntityComponent
    {
        public InkType CurrentInk { get; private set; }

        private readonly ChangeInkEvent _changeInkEvent = PlayerEvents.ChangeInkEvent;
        private int _prevIdx;
        
        
        public void Initialize(Entity entity)
        {
            CurrentInk = InkType.Red;
        }

        private void Start()
        {
            GameEventBus.RaiseEvent(_changeInkEvent.Init(CurrentInk));
        }

        private void Update()
        {
            var kb = Keyboard.current;
            if (kb == null) return;
            
            if (kb.digit1Key.wasPressedThisFrame) OnPressed(1);
            if (kb.digit2Key.wasPressedThisFrame) OnPressed(2);
            if (kb.digit3Key.wasPressedThisFrame) OnPressed(3);
            if (kb.digit4Key.wasPressedThisFrame) OnPressed(4);
            if (kb.digit5Key.wasPressedThisFrame) OnPressed(5);
            if (kb.digit6Key.wasPressedThisFrame) OnPressed(6);
            if (kb.digit7Key.wasPressedThisFrame) OnPressed(7);
            if (kb.digit8Key.wasPressedThisFrame) OnPressed(8);
            if (kb.digit9Key.wasPressedThisFrame) OnPressed(9);
            if (kb.digit0Key.wasPressedThisFrame) OnPressed(0);
        }

        private void OnPressed(int n)
        {
            if (InkLoadoutManager.Instance ==null ||
                (n > InkLoadoutManager.Instance.savedRemainingAmount.Count || 
                 _prevIdx == n - 1)) return;
            _prevIdx = n - 1;
            CurrentInk = InkLoadoutManager.Instance.savedRemainingAmount.ElementAt(n - 1).Key;
            GameEventBus.RaiseEvent(_changeInkEvent.Init(CurrentInk));
        }
    }
}