using KimMin.UI.Misc;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : PanelBase
{
    public override PanelType PanelType => PanelType.Default;

    [SerializeField] private GameObject settingPanel_img;
    [SerializeField] private Button setting_btn;

    private void Awake()
    {
        if (setting_btn != null)
        {
            setting_btn.onClick.RemoveAllListeners();
            setting_btn.onClick.AddListener(ToggleSetting);
        }

        if (settingPanel_img != null) settingPanel_img.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSetting();
        }
    }

    protected override void OnShow()
    {
        settingPanel_img.SetActive(true);
    }

    protected override void OnHide()
    {
        settingPanel_img.SetActive(false);
    }
    private void ToggleSetting()
    {
        if (settingPanel_img.activeSelf)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }
}