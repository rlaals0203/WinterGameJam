using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSupplySlot : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Image icon_img;
    [SerializeField] private TextMeshProUGUI name_txt;
    [SerializeField] private TextMeshProUGUI price_txt;
    [SerializeField] private TextMeshProUGUI desc_txt;

    [SerializeField] private TextMeshProUGUI type_txt;

    [SerializeField] private Button purchase_btn;

    private SupplySO currentSupply;
    private ShopManager shopManager;

    public void Setup(SupplySO supplyData, ShopManager manager)
    {
        currentSupply = supplyData;
        shopManager = manager;

        if (icon_img != null) icon_img.sprite = supplyData.icon;
        if (name_txt != null) name_txt.text = supplyData.SupplyName;
        if (price_txt != null) price_txt.text = $"{supplyData.price} Gold";
        if (desc_txt != null) desc_txt.text = supplyData.description;

        if (type_txt != null)
        {
            type_txt.text = GetKoreanType(supplyData.supplyType);
        }

        purchase_btn.onClick.RemoveAllListeners();
        purchase_btn.onClick.AddListener(OnClickPurchase);
    }

    private void OnClickPurchase()
    {
        shopManager.OnItemSelected(currentSupply);
    }

    private string GetKoreanType(SupplyType type) // 타입 한국말로 바꿔주기
    {
        switch (type)
        {
            case SupplyType.Palette: return "팔레트";
            case SupplyType.Brush: return "붓";
            case SupplyType.Extractor: return "추출기";
            default: return "";
        }
    }
}