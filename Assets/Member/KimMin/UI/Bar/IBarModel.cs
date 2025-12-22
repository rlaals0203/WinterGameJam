using System;

namespace KimMin.UI.Bar
{
    public interface IBarModel<T>
    {
        T MaxValue { get; }
        T CurrentValue { get; }
        event Action<T, T> OnValueChanged;
    }
}