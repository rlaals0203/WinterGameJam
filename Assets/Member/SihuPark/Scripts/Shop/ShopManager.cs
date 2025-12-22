using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform contentPanel;
    [SerializeField] private GameObject slotPrefab;

    [Header("Shop UI")]
    [SerializeField] private Button real_purchase_btn;

    [Header("Data")]
    [SerializeField] private List<SupplySO> allSupplyItems;

    private SupplySO currentSelectedSupply;

    private void Start()
    {
        GenerateShop();

        real_purchase_btn.onClick.RemoveAllListeners();
        real_purchase_btn.onClick.AddListener(OnRealPurchaseClick);
    }

    private void GenerateShop()
    {
        foreach (SupplySO item in allSupplyItems)
        {
            GameObject newSlot = Instantiate(slotPrefab, contentPanel);
            ShopSupplySlot slotScript = newSlot.GetComponent<ShopSupplySlot>();

            if (slotScript != null)
            {
                slotScript.Setup(item, this);
            }
        }
    }

    public void OnItemSelected(SupplySO item)
    {
        currentSelectedSupply = item;
    }

    private void OnRealPurchaseClick()
    {
        if (currentSelectedSupply == null) return;

        Debug.Log($"{currentSelectedSupply.SupplyName} 구매 완료 (가격: {currentSelectedSupply.price})");
    }
}