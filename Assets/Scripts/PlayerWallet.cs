using UnityEngine;
using TMPro;
public class PlayerWallet : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private TMP_Text moneyText;
    private int currentMoney = 0;

    private void OnEnable()
    {
        SellZone.OnLogSold += AddMoney;
    }

    private void OnDisable()
    {
        SellZone.OnLogSold -= AddMoney;
    }

    void Start()
    {
        UpdateMoneyUI();
    }

    private void AddMoney(int amount)
    {
        currentMoney += amount;
        Debug.Log("Player now has: $" + currentMoney);
        UpdateMoneyUI();
    }

    private void UpdateMoneyUI()
    {
        moneyText?.SetText("$ " + currentMoney);
    }
}

