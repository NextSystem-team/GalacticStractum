using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSoundEmitter : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
    }
}
