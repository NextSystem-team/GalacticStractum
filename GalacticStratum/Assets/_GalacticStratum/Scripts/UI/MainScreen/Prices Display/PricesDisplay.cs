using UnityEngine;

public class PricesDisplay : MonoBehaviour
{
    [SerializeField] private RectTransform lechatelieritePriceDisplay;
    [SerializeField] private RectTransform elaliitePriceDisplay;
    [SerializeField] private RectTransform whitlockitePriceDisplay;
    [SerializeField] private RectTransform beskariumPriceDisplay;

    void Start()
    {
        UpdateDisplaySize(AsteroidData.ResourceType.Lechatelierite);
        UpdateDisplaySize(AsteroidData.ResourceType.Elaliite);
        UpdateDisplaySize(AsteroidData.ResourceType.Whitlockite);
        UpdateDisplaySize(AsteroidData.ResourceType.Beskarium);
    }

    private void UpdateDisplaySize(AsteroidData.ResourceType resourceType)
    {
        float minSize = 70f;
        float maxSize = 470f;

        float newSize;

        float pricePercentage;

        switch (resourceType)
        {
            case AsteroidData.ResourceType.Lechatelierite:
                pricePercentage = Mathf.InverseLerp(ResourcesPriceManager.MIN_PRICE, ResourcesPriceManager.MAX_PRICE, SaveManager.currentGameData.lechatelieritePrice);
                newSize = Mathf.Lerp(minSize, maxSize, pricePercentage);
                lechatelieritePriceDisplay.sizeDelta = new Vector2(lechatelieritePriceDisplay.sizeDelta.x, newSize);
                break;
            case AsteroidData.ResourceType.Elaliite:
                pricePercentage = Mathf.InverseLerp(ResourcesPriceManager.MIN_PRICE, ResourcesPriceManager.MAX_PRICE, SaveManager.currentGameData.elaliitePrice);
                newSize = Mathf.Lerp(minSize, maxSize, pricePercentage);
                elaliitePriceDisplay.sizeDelta = new Vector2(elaliitePriceDisplay.sizeDelta.x, newSize);
                break;
            case AsteroidData.ResourceType.Whitlockite:
                pricePercentage = Mathf.InverseLerp(ResourcesPriceManager.MIN_PRICE, ResourcesPriceManager.MAX_PRICE, SaveManager.currentGameData.whitlockitePrice);
                newSize = Mathf.Lerp(minSize, maxSize, pricePercentage);
                whitlockitePriceDisplay.sizeDelta = new Vector2(whitlockitePriceDisplay.sizeDelta.x, newSize);
                break;
            case AsteroidData.ResourceType.Beskarium:
                pricePercentage = Mathf.InverseLerp(ResourcesPriceManager.MIN_PRICE, ResourcesPriceManager.MAX_PRICE, SaveManager.currentGameData.beskariumPrice);
                newSize = Mathf.Lerp(minSize, maxSize, pricePercentage);
                beskariumPriceDisplay.sizeDelta = new Vector2(beskariumPriceDisplay.sizeDelta.x, newSize);
                break;
            default:
                break;
        }
    }
}
