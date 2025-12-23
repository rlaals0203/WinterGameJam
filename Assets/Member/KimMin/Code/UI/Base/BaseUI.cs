using System;
using UnityEngine;

namespace KimMin.UI.Base
{
    public abstract class BaseUI : MonoBehaviour
    {
        protected virtual  void BindUIEvents() { }
        protected virtual void UnbindUIEvents() { }
        protected virtual void EnableUI() { }
        protected virtual void DisableUI() { }

        private void OnEnable()
        {
            BindUIEvents();
            EnableUI();
        }

        private void OnDisable()
        {
            UnbindUIEvents();
            DisableUI();
        }
    }
}