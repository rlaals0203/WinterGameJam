using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSupplySlot : MonoBehaviour
{
    [Header("My Data")]
    public SupplySO mySupplyData;

    [Header("Settings")]
    [SerializeField] private ShopManager shopManager; 
    [SerializeField] private Button myButton;

    [Header("My UI")]
    [SerializeField] private Image myIcon;
    [SerializeField] private TextMeshProUGUI myName;

    private void Start()
    {
        if (mySupplyData != null)
        {
            if (myIcon != null) myIcon.sprite = mySupplyData.icon;
            if (myName != null) myName.text = mySupplyData.SupplyName;
        }

        myButton.onClick.RemoveAllListeners();
        myButton.onClick.AddListener(() => {
            shopManager.UpdateInfoPanel(mySupplyData);
        });
    }
}