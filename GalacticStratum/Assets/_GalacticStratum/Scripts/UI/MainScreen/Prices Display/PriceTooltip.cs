using TMPro;
using UnityEngine;

public class PriceTooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Vector2 offset = new(10f, 10f);
    [SerializeField] private Canvas parentCanvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private RectTransform canvasRectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (parentCanvas != null)
        {
            canvasRectTransform = parentCanvas.GetComponent<RectTransform>();
        }
    }

    private void Update()
    {
        if (canvasGroup.alpha != 0)
        {
            Vector2 mousePosition = Input.mousePosition;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRectTransform,
                mousePosition,
                Camera.main,
                out Vector2 localPoint
            );

            rectTransform.anchoredPosition = localPoint /*+ offset*/;
        }
    }

    public void ShowTooltip(int price)
    {
        priceText.text = "$" + price.ToString("N0");
        canvasGroup.alpha = 1f;
    }

    public void HideTooltip()
    {
        canvasGroup.alpha = 0f;
    }
}
