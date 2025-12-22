using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("Info Panel")]
    [SerializeField] private Image info_icon_img;
    [SerializeField] private TextMeshProUGUI info_name_txt;
    [SerializeField] private TextMeshProUGUI info_desc_txt;
    [SerializeField] private TextMeshProUGUI info_price_txt;
    [SerializeField] private TextMeshProUGUI info_type_txt; 

    [Header("Buy Button")]
    [SerializeField] private Button buy_confirm_btn;

    private SupplySO currentSelectedSupply;

    private void Start()
    {
        buy_confirm_btn.interactable = false;

        buy_confirm_btn.onClick.RemoveAllListeners();
        buy_confirm_btn.onClick.AddListener(OnBuyConfirmClick);
    }

    public void UpdateInfoPanel(SupplySO item)
    {
        currentSelectedSupply = item;

        buy_confirm_btn.interactable = true;

        info_icon_img.sprite = item.icon;
        info_name_txt.text = item.SupplyName;
        info_desc_txt.text = item.description;
        info_price_txt.text = $"{item.price} Gold";

        // 타입 표시 한글로 바꿈
        if (item.supplyType == SupplyType.Palette) info_type_txt.text = "팔레트";
        else if (item.supplyType == SupplyType.Brush) info_type_txt.text = "붓";
        else if (item.supplyType == SupplyType.Extractor) info_type_txt.text = "추출기";

        Debug.Log($"정보창 갱신됨: {item.SupplyName}");
    }

    private void OnBuyConfirmClick()
    {
        if (currentSelectedSupply == null) return;
        
        // 결제 요청
        int cost = currentSelectedSupply.price;

        if (MoneyManager.Instance.TrySpendMoney(cost))
        {
            Debug.Log($"[구매 성공] {currentSelectedSupply.SupplyName}");

            // 얻은 아이템은 여기에서 처리
        }
        else
        {
            Debug.Log("[구매 실패] 돈엇ㅂ음");
        }
    }
}