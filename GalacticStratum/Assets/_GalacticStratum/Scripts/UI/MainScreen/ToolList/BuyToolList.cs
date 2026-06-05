using UnityEngine;
using UnityEngine.UI;

public class BuyToolList : MonoBehaviour
{
    [Header("All Tools Data")]
    [SerializeField] private ToolListData toolListData;

    [Header("UI Elements")]
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private ToolInfoTooltip tooltip;

    private void Start()
    {
        DisplayToolsByCategory(ToolData.ToolType.All);
    }

    public void DisplayToolsByCategory(ToolData.ToolType category)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (ToolData tool in toolListData.Tools)
        {
            if (category == ToolData.ToolType.All || tool.Type == category)
            {
                if (!SaveManager.CheckIfHasTool(tool.ToolID))
                {
                    CreateToolButton(tool);
                }
            }
        }
    }

    private void CreateToolButton(ToolData tool)
    {
        GameObject button = Instantiate(buttonPrefab, transform);
        BuyToolButton toolButton = button.GetComponent<BuyToolButton>();

        toolButton.tool = tool;
        toolButton.tooltip = tooltip;
    }
}
