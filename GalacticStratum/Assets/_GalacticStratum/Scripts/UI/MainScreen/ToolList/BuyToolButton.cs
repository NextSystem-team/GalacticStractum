using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyToolButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ToolData tool;
    [SerializeField] private Image icon;
    
    public ToolInfoTooltip tooltip;

    private void Start()
    {
        icon.sprite = tool.Icon;

        GetComponent<Button>().onClick.AddListener(BuyItem);
    }

    private void BuyItem()
    {
        if (SaveManager.currentPlayerData.moneyAmount >= tool.Price)
        {
            SaveManager.currentPlayerData.toolsObtained.Add(tool.ToolID);
            Destroy(gameObject);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.ShowTooltip(tool);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();
    }
}
