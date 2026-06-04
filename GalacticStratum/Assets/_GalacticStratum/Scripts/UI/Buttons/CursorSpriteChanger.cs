using UnityEngine;
using UnityEngine.EventSystems;

public class CursorSpriteChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Texture2D pointerCursor;

    public void OnPointerEnter(PointerEventData onEnter)
    {
        if (CursorSpriteManager.currentCursorSprite == null)
        {
            Cursor.SetCursor(pointerCursor, Vector2.zero, CursorMode.Auto);
            CursorSpriteManager.currentCursorSprite = pointerCursor;
            CursorSpriteManager.currentCursorHotspot = Vector2.zero;
        }
    }

    public void OnPointerExit(PointerEventData onExit)
    {
        if (CursorSpriteManager.currentCursorSprite == pointerCursor)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            CursorSpriteManager.currentCursorSprite = null;
            CursorSpriteManager.currentCursorHotspot = Vector2.zero;
        }
    }
}
