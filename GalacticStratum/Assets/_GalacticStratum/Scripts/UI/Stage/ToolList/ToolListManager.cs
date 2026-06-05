using UnityEngine;
using UnityEngine.UI;

public class ToolListManager : MonoBehaviour
{
    [Header("All Tools Data")]
    [SerializeField] private ToolListData toolListData;

    [Header("UI Elements")]
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Button buttonCategoryAll;
    [SerializeField] private Button buttonCategoryLocators;
    [SerializeField] private Button buttonCategoryMiners;
    [SerializeField] private Button buttonCategoryOthers;

    private void Start()
    {
        buttonCategoryAll.onClick.AddListener(() => DisplayToolsByCategory(ToolData.ToolType.All));
        buttonCategoryMiners.onClick.AddListener(() => DisplayToolsByCategory(ToolData.ToolType.Miner));
        buttonCategoryLocators.onClick.AddListener(() => DisplayToolsByCategory(ToolData.ToolType.Locator));
        buttonCategoryOthers.onClick.AddListener(() => DisplayToolsByCategory(ToolData.ToolType.Other));

        DisplayToolsByCategory(ToolData.ToolType.All);
    }

    private void DisplayToolsByCategory(ToolData.ToolType category)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (ToolData tool in toolListData.Tools)
        {
            if (category == ToolData.ToolType.All || tool.Type == category)
            {
                if (SaveManager.CheckIfHasTool(tool.ToolID))
                {
                    CreateToolButton(tool);
                }
            }
        }
    }

    private void CreateToolButton(ToolData tool)
    {
        GameObject button = Instantiate(buttonPrefab, transform);
        ToolButton toolButton = button.GetComponent<ToolButton>();

        toolButton.tool = tool;
    }
}
