using System;
using UnityEngine;

namespace KimMin.UI.Tooltip
{
    public abstract class BaseTooltip : MonoBehaviour
    {
        public abstract Type DataType { get; }
        public RectTransform RectTransform => transform as RectTransform;
        public abstract void Show(object data);
        public abstract void Hide();
    }

    public abstract class BaseTooltip<TData> : BaseTooltip
    {
        public override Type DataType => typeof(TData);
        public sealed override void Show(object data)
        {
            Show((TData)data);
        }
        protected abstract void Show(TData data);
    }
}