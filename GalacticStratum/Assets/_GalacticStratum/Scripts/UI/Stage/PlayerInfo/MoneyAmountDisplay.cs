using System.Globalization;
using TMPro;
using UnityEngine;

public class MoneyAmountDisplay : MonoBehaviour
{
    private TextMeshProUGUI moneyAmountText;
    private int moneyAmount = -1;
        
    private void Start()
    {
        moneyAmountText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (moneyAmount != SaveManager.currentPlayerData.moneyAmount)
        {
            moneyAmount = SaveManager.currentPlayerData.moneyAmount;
            moneyAmountText.text = "$" + moneyAmount.ToString("N0", CultureInfo.CurrentCulture);
        }
    }
}
