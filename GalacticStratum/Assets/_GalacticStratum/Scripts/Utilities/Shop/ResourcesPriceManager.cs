using UnityEngine;

public static class ResourcesPriceManager
{
    private const int ITEMS_PER_SATURATION_LEVEL = 300;
    private const int MAX_SATURATION_LEVEL = 3;

    private const int MIN_PRICE_FLUCTUATION = -6;
    private const int MAX_PRICE_FLUCTUATION = 9;

    private static readonly int[] SaturationPenalties = { 0, 5, 12, 20 };

    public const int MIN_PRICE = 5;
    public const int MAX_PRICE = 80;

    public static void RegisterSale(AsteroidData.ResourceType resource, int amountSold)
    {
        GameData data = SaveManager.currentGameData;

        switch (resource) { 
            case AsteroidData.ResourceType.Lechatelierite:
                data.lechatelieriteSaturationLevel += amountSold / ITEMS_PER_SATURATION_LEVEL;
                data.lechatelieriteSaturationLevel = Mathf.Clamp(data.lechatelieriteSaturationLevel, 0, MAX_SATURATION_LEVEL);
                break;
            case AsteroidData.ResourceType.Elaliite:
                data.elaliiteSaturationLevel += amountSold / ITEMS_PER_SATURATION_LEVEL;
                data.elaliiteSaturationLevel = Mathf.Clamp(data.elaliiteSaturationLevel, 0, MAX_SATURATION_LEVEL);
                break;
            case AsteroidData.ResourceType.Whitlockite:
                data.whitlockiteSaturationLevel += amountSold / ITEMS_PER_SATURATION_LEVEL;
                data.whitlockiteSaturationLevel = Mathf.Clamp(data.whitlockiteSaturationLevel, 0, MAX_SATURATION_LEVEL);
                break;
            case AsteroidData.ResourceType.Beskarium:
                data.beskariumSaturationLevel += amountSold / ITEMS_PER_SATURATION_LEVEL;
                data.beskariumSaturationLevel = Mathf.Clamp(data.beskariumSaturationLevel, 0, MAX_SATURATION_LEVEL);
                break;
            default:
                break;
        }
    }

    public static void UpdateResourcesPrices()
    {
        GameData data = SaveManager.currentGameData;

        data.lechatelieritePrice = CalculateNewPrice(data.lechatelieritePrice, data.lechatelieriteSaturationLevel);
        data.elaliitePrice = CalculateNewPrice(data.elaliitePrice, data.elaliiteSaturationLevel);
        data.whitlockitePrice = CalculateNewPrice(data.whitlockitePrice, data.whitlockiteSaturationLevel);
        data.beskariumPrice = CalculateNewPrice(data.beskariumPrice, data.beskariumSaturationLevel);

        data.lechatelieriteSaturationLevel = Mathf.Max(0, data.lechatelieriteSaturationLevel - 1);
        data.elaliiteSaturationLevel = Mathf.Max(0, data.elaliiteSaturationLevel - 1);
        data.whitlockiteSaturationLevel = Mathf.Max(0, data.whitlockiteSaturationLevel - 1);
        data.beskariumSaturationLevel = Mathf.Max(0, data.beskariumSaturationLevel - 1);
    }

    private static int CalculateNewPrice(int currentPrice, int saturationLevel)
    {
        int priceFluctuation = Random.Range(MIN_PRICE_FLUCTUATION, MAX_PRICE_FLUCTUATION + 1);
        int basePrice = currentPrice + priceFluctuation;

        int saturationPenalty = SaturationPenalties[saturationLevel];

        int finalPrice = basePrice - saturationPenalty;
        return Mathf.Clamp(finalPrice, MIN_PRICE, MAX_PRICE);
    }
}
