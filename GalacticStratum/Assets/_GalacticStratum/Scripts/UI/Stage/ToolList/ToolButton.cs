using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolButton : MonoBehaviour
{
    public ToolData tool;
    [SerializeField] private Image icon;

    private void Start()
    {
        icon.sprite = tool.Icon;

        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (SaveManager.currentPlayerData.moneyAmount >= tool.UsePrice)
        {
            SetCursor(tool.Icon.texture);
            GlobalEvents.OnToolSelected?.Invoke(tool.Tool);
        }
    }

    private void SetCursor(Texture2D toolSprite)
    {
        Vector2 newHotspot = new(toolSprite.width / 2, toolSprite.height / 2);
        Cursor.SetCursor(toolSprite, newHotspot, CursorMode.Auto);
        CursorSpriteManager.currentCursorSprite = toolSprite;
        CursorSpriteManager.currentCursorHotspot = newHotspot;
    }
}
