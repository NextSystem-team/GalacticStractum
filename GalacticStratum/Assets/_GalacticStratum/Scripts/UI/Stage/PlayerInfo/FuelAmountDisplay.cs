using TMPro;
using UnityEngine;

public class FuelAmountDisplay : MonoBehaviour
{
    public PlayerStorage playerStorage;

    private TextMeshProUGUI fuelAmountText;
    private float fuelAmount;

    private void Start()
    {
        fuelAmountText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (playerStorage != null)
        {
            if (fuelAmount != playerStorage.fuelAmount)
            {
                UpdateFuelAmount();
                fuelAmount = playerStorage.fuelAmount;
            }
        }
    }

    private void UpdateFuelAmount()
    {
        int maxFuelAmount = playerStorage.MaxFuel;
        float currentFuelAmount = playerStorage.fuelAmount;

        float fuelPercentage = (currentFuelAmount / maxFuelAmount) * 100f;

        fuelAmountText.text = $"{fuelPercentage:F1}%";
    }
}
