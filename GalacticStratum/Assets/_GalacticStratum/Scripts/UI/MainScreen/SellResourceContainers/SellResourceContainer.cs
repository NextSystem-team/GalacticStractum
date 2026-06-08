using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellResourceContainer : MonoBehaviour
{

    private struct ResourceData
    {
        public int Amount { get; set; }
        public int Price { get; set; }
    }

    [SerializeField] private AsteroidData.ResourceType resourceType;
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

    private void SellResource(int sellAmount, AsteroidData.ResourceType resourceType)
    {
        if (sellAmount <= 0) return;

        ResourceData resourceData = GetResourceData(resourceType);

        if (resourceData.Amount > 0)
        {
            int resourcesSelled;

            if (sellAmount > resourceData.Amount)
            {
                resourcesSelled = resourceData.Amount;
                amountText.text = DecreasePlayerResourceAmount(resourcesSelled, resourceType).ToString();
            }
            else
            {
                resourcesSelled = sellAmount;
                amountText.text = amountText.text = DecreasePlayerResourceAmount(resourcesSelled, resourceType).ToString();
            }

            SaveManager.currentPlayerData.moneyAmount += resourcesSelled * resourceData.Price;
            ResourcesPriceManager.RegisterSale(resourceType, resourcesSelled);

            AudioManager.Instance.PlaySFX("Sell");
        }
    }

    private ResourceData GetResourceData(AsteroidData.ResourceType resource)
    {
        switch (resource)
        {
            case AsteroidData.ResourceType.Beskarium:
                return new ResourceData
                {
                    Amount = SaveManager.currentPlayerData.beskariumAmount,
                    Price = SaveManager.currentGameData.beskariumPrice
                };
            case AsteroidData.ResourceType.Whitlockite:
                return new ResourceData
                {
                    Amount = SaveManager.currentPlayerData.whitlockiteAmount,
                    Price = SaveManager.currentGameData.whitlockitePrice
                };
            case AsteroidData.ResourceType.Lechatelierite:
                return new ResourceData
                {
                    Amount = SaveManager.currentPlayerData.lechatelieriteAmount,
                    Price = SaveManager.currentGameData.lechatelieritePrice
                };
            case AsteroidData.ResourceType.Elaliite:
                return new ResourceData
                {
                    Amount = SaveManager.currentPlayerData.elaliiteAmount,
                    Price = SaveManager.currentGameData.elaliitePrice
                };
            default:
                return default;
        }
    }

    private int DecreasePlayerResourceAmount(int amount, AsteroidData.ResourceType resource)
    {
        switch (resource)
        {
            case AsteroidData.ResourceType.Beskarium:
                SaveManager.currentPlayerData.beskariumAmount -= amount;
                return SaveManager.currentPlayerData.beskariumAmount;
            case AsteroidData.ResourceType.Whitlockite:
                SaveManager.currentPlayerData.whitlockiteAmount -= amount;
                return SaveManager.currentPlayerData.whitlockiteAmount;
            case AsteroidData.ResourceType.Lechatelierite:
                SaveManager.currentPlayerData.lechatelieriteAmount -= amount;
                return SaveManager.currentPlayerData.lechatelieriteAmount;
            case AsteroidData.ResourceType.Elaliite:
                SaveManager.currentPlayerData.elaliiteAmount -= amount;
                return SaveManager.currentPlayerData.elaliiteAmount;
            default:
                return 0;
        }
    }
}
