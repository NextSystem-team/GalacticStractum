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

            SaveManager.currentPlayerData.moneyAmount -= tool.Price;

            AudioManager.Instance.PlaySFX("Buy");

            Destroy(gameObject);
        }
        else
        {
            AudioManager.Instance.PlaySFX("Invalid");
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

    private void OnDestroy()
    {
        tooltip.HideTooltip();
    }
}
