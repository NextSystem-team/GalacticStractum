using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DefaultButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Button button;
    private TextMeshProUGUI text;
    private Color defaultTextColor;
    private Material material;

    [SerializeField] private float defaultScale;
    [SerializeField] private float hoveredScale;
    [SerializeField] private float pressedScale;

    [ColorUsage(false, true)] [SerializeField] private Color hoverHighlightColor;
    [SerializeField] private Color hoverTextColor;

    private Color currentTextColor;
    private float currentScale;
    private bool isPressed;

    private void Start()
    {
        button = GetComponent<Button>();
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        defaultTextColor = text.color;
        material = text.fontMaterial;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isPressed)
        {
            button.transform.DOScale(hoveredScale, 0.2f).SetUpdate(true);
            text.color = hoverTextColor;
        }

        material.DOColor(hoverHighlightColor, ShaderUtilities.ID_FaceColor, 0.2f).SetUpdate(true);
        currentScale = hoveredScale;
        currentTextColor = hoverTextColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isPressed)
        {
            button.transform.DOScale(defaultScale, 0.2f).SetUpdate(true);
            text.color = defaultTextColor;
        }
       
        material.DOColor(Color.white, ShaderUtilities.ID_FaceColor, 0.2f).SetUpdate(true);
        currentScale = defaultScale;
        currentTextColor = defaultTextColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        text.color = Color.black;
        button.transform.DOScale(pressedScale, 0.3f).SetUpdate(true);
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        text.color = defaultTextColor;
        button.transform.DOScale(currentScale, 0.3f).SetUpdate(true);
        isPressed = false;
    }
}
