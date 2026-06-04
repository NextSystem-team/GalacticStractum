using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellResourceContainer : MonoBehaviour
{
    private enum ResourceType
    {
        Beskarium,
        Whitlockite,
        Lechatelierite,
        Elaliite
    }

    private struct ResourceData
    {
        public int Amount { get; set; }
        public int Price { get; set; }
    }

    [SerializeField] private ResourceType resourceType;
    [SerializeField] private Button sellOneButton;
    [SerializeField] private Button sellAllButton;
    [SerializeField] private Button sellCustomAmount;
    [SerializeField] private TMP_InputField customAmountField;

    [SerializeField] private TextMeshProUGUI amountText;

    private void Start()
    {
        sellOneButton.onClick.AddListener(() => SellResource(1, resourceType));
        sellAllButton.onClick.AddListener(() => SellResource(GetResourceData(resourceType).Amount, resourceType));
        sellCustomAmount.onClick.AddListener(() =>
        {
            int.TryParse(customAmountField.text, out int customAmount);
            SellResource(customAmount, resourceType);
        });

        amountText.text = GetResourceData(resourceType).Amount.ToString();
    }

    private void SellResource(int sellAmount, ResourceType resourceType)
    {
        if (sellAmount <= 0) return;

        ResourceData resourceData = GetResourceData(resourceType);

        if (resourceData.Amount > 0)
        {
            int resourcesSelled;

            if (sellAmount > resourceData.Amount)
            {
                resourcesSelled = resourceData.Amount;
                amountText.text = DecreasePlayerResourAmount(resourcesSelled, resourceType).ToString();
            }
            else
            {
                resourcesSelled = sellAmount;
                amountText.text = amountText.text = DecreasePlayerResourAmount(resourcesSelled, resourceType).ToString();
            }

            SaveManager.currentPlayerData.moneyAmount += resourcesSelled * resourceData.Price;
        }
    }

    private ResourceData GetResourceData(ResourceType resource)
    {
        switch (resource)
        {
            case ResourceType.Beskarium:
                return new ResourceData
                {
                    Amount = SaveManager.currentPlayerData.beskariumAmount,
                    Price = SaveManager.currentGameData.beskariumPrice
                };
            case ResourceType.Whitlockite:
                return new ResourceData
                {
                    Amount = SaveManager.currentPlayerData.whitlockiteAmount,
                    Price = SaveManager.currentGameData.whitlockitePrice
                };
            case ResourceType.Lechatelierite:
                return new ResourceData
                {
                    Amount = SaveManager.currentPlayerData.lechatelieriteAmount,
                    Price = SaveManager.currentGameData.lechatelieritePrice
                };
            case ResourceType.Elaliite:
                return new ResourceData
                {
                    Amount = SaveManager.currentPlayerData.elaliiteAmount,
                    Price = SaveManager.currentGameData.elaliitePrice
                };
            default:
                return default;
        }
    }

    private int DecreasePlayerResourAmount(int amount, ResourceType resource)
    {
        switch (resource)
        {
            case ResourceType.Beskarium:
                SaveManager.currentPlayerData.beskariumAmount -= amount;
                return SaveManager.currentPlayerData.beskariumAmount;
            case ResourceType.Whitlockite:
                SaveManager.currentPlayerData.whitlockiteAmount -= amount;
                return SaveManager.currentPlayerData.whitlockiteAmount;
            case ResourceType.Lechatelierite:
                SaveManager.currentPlayerData.lechatelieriteAmount -= amount;
                return SaveManager.currentPlayerData.lechatelieriteAmount;
            case ResourceType.Elaliite:
                SaveManager.currentPlayerData.elaliiteAmount -= amount;
                return SaveManager.currentPlayerData.elaliiteAmount;
            default:
                return 0;
        }
    }
}
