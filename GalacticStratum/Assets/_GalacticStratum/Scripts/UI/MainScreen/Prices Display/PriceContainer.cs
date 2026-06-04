using UnityEngine;
using UnityEngine.EventSystems;

public class PriceContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private AsteroidData.ResourceType resourceType;
    [SerializeField] private PriceTooltip tooltip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.ShowTooltip(GetResourcePrice(resourceType));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();
    }

    private int GetResourcePrice(AsteroidData.ResourceType type)
    {
        switch (type)
        {
            case AsteroidData.ResourceType.Lechatelierite:
                return SaveManager.currentGameData.lechatelieritePrice;
            case AsteroidData.ResourceType.Elaliite:
                return SaveManager.currentGameData.elaliitePrice;
            case AsteroidData.ResourceType.Beskarium:
                return SaveManager.currentGameData.beskariumPrice;
            case AsteroidData.ResourceType.Whitlockite:
                return SaveManager.currentGameData.whitlockitePrice;
            default:
                return 0;
        }
    }
}
