using Code.Core;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyManager : MonoSingleton<MoneyManager>
{
    public static MoneyManager Instance;

    [Header("Settings")]
    [SerializeField] private int currentMoney = 0;
    [SerializeField] private Text money_txt; 

    private void Start()
    {
        UpdateMoneyUI();
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        UpdateMoneyUI();
    }

    public bool TrySpendMoney(int amount)
    {
        if (currentMoney >= amount)
        {
            currentMoney -= amount;
            UpdateMoneyUI();
            return true;
        }
        else
        {
            Debug.Log("Money is not enough");
            return false; 
        }
    }

    private void UpdateMoneyUI()
    {
        if (money_txt != null)
        {
            money_txt.text = $"{currentMoney:N0} G";
        }
    }
}