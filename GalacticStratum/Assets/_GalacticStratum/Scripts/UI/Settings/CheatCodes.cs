using UnityEngine;

public class CheatCodes : MonoBehaviour
{
    [SerializeField] private ToolListData toolListData;

    private const string CHEAT_MONEY = "spaceconnect";
    private const string CHEAT_TOOLS = "globalsolution";

    private string inputHistory = "";

    private const int MAX_HISTORY_LENGTH = 15;

    private void Update()
    {
        if (!string.IsNullOrEmpty(Input.inputString))
        {
            inputHistory += Input.inputString.ToLower();

            if (inputHistory.Length > MAX_HISTORY_LENGTH)
            {
                inputHistory = inputHistory.Substring(inputHistory.Length - MAX_HISTORY_LENGTH);
            }

            CheckCheats();
        }
    }

    private void CheckCheats()
    {
        if (inputHistory.EndsWith(CHEAT_MONEY))
        {
            ActivateMoneyCheat();
            ClearHistory();
        }
        else if (inputHistory.EndsWith(CHEAT_TOOLS))
        {
            ActivateToolsCheat();
            ClearHistory();
        }
    }

    private void ClearHistory()
    {
        inputHistory = "";
    }

    private void ActivateMoneyCheat()
    {
        if (SaveManager.currentPlayerData == null) return;

        SaveManager.currentPlayerData.moneyAmount += 1000000;

        Debug.Log("Dinheiro adicionado!");
    }

    private void ActivateToolsCheat()
    {
        if (SaveManager.currentPlayerData == null) return;
        foreach (ToolData tool in toolListData.Tools)
        {
            if (!SaveManager.currentPlayerData.toolsObtained.Contains(tool.ToolID))
            {
                SaveManager.currentPlayerData.toolsObtained.Add(tool.ToolID);
            }
        }
        Debug.Log("Todas as ferramentas desbloqueadas!");
    }
}
