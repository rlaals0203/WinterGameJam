using UnityEngine;

public abstract class PanelBase : MonoBehaviour
{
    public abstract PanelType PanelType { get; }

    protected bool panelOpen; // 패널 열렸는지 안열렸는지만 판단해서 추후 같이 열리는 거 막을 예정

    public virtual void Show()
    {
        if (panelOpen) return;

        panelOpen = true;

        OnShow();
    }

    public virtual void Hide()
    {
        if (!panelOpen) return;

        panelOpen = false;

        OnHide();
    }

    public bool PanelOpen()
    {
        return panelOpen;
    }

    protected abstract void OnShow();

    protected abstract void OnHide();

}
