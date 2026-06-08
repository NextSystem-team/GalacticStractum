using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopContainer : MonoBehaviour
{
    [SerializeField] private RectTransform shopBox;
    [SerializeField] private Button switchToBuyButton;
    [SerializeField] private Button switchToSellButton;

    [SerializeField] private Button sellAllResources;
    [SerializeField] private TextMeshProUGUI lechatelieriteAmountText;
    [SerializeField] private TextMeshProUGUI elaliiteAmountText;
    [SerializeField] private TextMeshProUGUI beskariumAmountText;
    [SerializeField] private TextMeshProUGUI whitlockiteAmountText;

    private void Start()
    {
        switchToBuyButton.onClick.AddListener(SwitchToBuySession);
        switchToSellButton.onClick.AddListener(SwitchToSellSession);
        sellAllResources.onClick.AddListener(SellAllResources);
    }

    private void SwitchToBuySession()
    {
        shopBox.DOAnchorPosX(0, 0.3f);
        AudioManager.Instance.PlaySFX("Sweep");
    }

    private void SwitchToSellSession()
    {
        shopBox.DOAnchorPosX(-946, 0.3f);
        AudioManager.Instance.PlaySFX("Sweep");
    }

    private void SellAllResources()
    {
        int lechatelieriteAmount = SaveManager.currentPlayerData.lechatelieriteAmount;
        int elaliiteAmount = SaveManager.currentPlayerData.elaliiteAmount;
        int beskariumAmount = SaveManager.currentPlayerData.beskariumAmount;
        int whitlockiteAmount = SaveManager.currentPlayerData.whitlockiteAmount;

        SaveManager.currentPlayerData.lechatelieriteAmount = 0;
        SaveManager.currentPlayerData.elaliiteAmount = 0;
        SaveManager.currentPlayerData.beskariumAmount = 0;
        SaveManager.currentPlayerData.whitlockiteAmount = 0;

        lechatelieriteAmountText.text = "0";
        elaliiteAmountText.text = "0";
        beskariumAmountText.text = "0";
        whitlockiteAmountText.text = "0";

        int totalSellValue = lechatelieriteAmount * SaveManager.currentGameData.lechatelieritePrice + 
                             elaliiteAmount * SaveManager.currentGameData.elaliitePrice + 
                             beskariumAmount * SaveManager.currentGameData.beskariumPrice + 
                             whitlockiteAmount * SaveManager.currentGameData.whitlockitePrice;

        SaveManager.currentPlayerData.moneyAmount += totalSellValue;

        ResourcesPriceManager.RegisterSale(AsteroidData.ResourceType.Lechatelierite, lechatelieriteAmount);
        ResourcesPriceManager.RegisterSale(AsteroidData.ResourceType.Elaliite, elaliiteAmount);
        ResourcesPriceManager.RegisterSale(AsteroidData.ResourceType.Beskarium, beskariumAmount);
        ResourcesPriceManager.RegisterSale(AsteroidData.ResourceType.Whitlockite, whitlockiteAmount);

        AudioManager.Instance.PlaySFX("Sell");
    }
}
