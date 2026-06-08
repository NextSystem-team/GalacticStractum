using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSoundEmitter : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX("ButtonHover");
    }
}
