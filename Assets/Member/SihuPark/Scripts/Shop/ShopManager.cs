using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [Header("Info Panel")]
    [SerializeField] private Image info_icon_img;
    [SerializeField] private TextMeshProUGUI info_name_txt;
    [SerializeField] private TextMeshProUGUI info_desc_txt;
    [SerializeField] private TextMeshProUGUI info_price_txt;
    [SerializeField] private TextMeshProUGUI info_type_txt;

    [Header("Buy Button")]
    [SerializeField] private Button buy_confirm_btn;
    [SerializeField] private TextMeshProUGUI buy_btn_txt;

    private SupplySO currentSelectedSupply;

    private HashSet<string> purchasedItems = new HashSet<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        buy_confirm_btn.interactable = false;
        if (buy_btn_txt != null) buy_btn_txt.text = "구매하기";

        buy_confirm_btn.onClick.RemoveAllListeners();
        buy_confirm_btn.onClick.AddListener(OnBuyConfirmClick);
    }

    public void UpdateInfoPanel(SupplySO item)
    {
        currentSelectedSupply = item;

        info_icon_img.sprite = item.icon;
        info_name_txt.text = item.SupplyName;
        info_desc_txt.text = item.description;
        info_price_txt.text = $"{item.price} Gold";

        if (item.supplyType == SupplyType.Palette) info_type_txt.text = "팔레트";
        else if (item.supplyType == SupplyType.Brush) info_type_txt.text = "붓";
        else if (item.supplyType == SupplyType.Extractor) info_type_txt.text = "추출기";

        CheckPurchaseState();
    }

    private void CheckPurchaseState()
    {
        bool isPurchased = purchasedItems.Contains(currentSelectedSupply.SupplyName);

        if (isPurchased)
        {
            buy_confirm_btn.interactable = false;
            if (buy_btn_txt != null) buy_btn_txt.text = "구매완료";
        }
        else
        {
            buy_confirm_btn.interactable = true;
            if (buy_btn_txt != null) buy_btn_txt.text = "구매하기";
        }
    }

    private void OnBuyConfirmClick()
    {
        if (currentSelectedSupply == null) return;

        if (MoneyManager.Instance.TrySpendMoney(currentSelectedSupply.price))
        {
            purchasedItems.Add(currentSelectedSupply.SupplyName);


            CheckPurchaseState();
        }
    }
}