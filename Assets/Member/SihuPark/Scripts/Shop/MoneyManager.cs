using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    [Header("Settings")]
    [SerializeField] private int currentMoney = 1000;
    [SerializeField] private TextMeshProUGUI money_txt; 

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

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
            money_txt.text = "Money : " + $"{currentMoney:N0} G";
        }
    }
}