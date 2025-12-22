using System;
using KimMin.UI.Controller;
using UnityEngine;

namespace KimMin.UI.Core
{
    public class UIManager : MonoBehaviour
    {
        public TooltipController Tooltip { get; private set; }

        private void Awake()
        {
            Tooltip = GetComponentInChildren<TooltipController>(true);
        }
    }
}