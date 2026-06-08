using DG.Tweening;
using TMPro;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    private RectTransform myRect;
    private TextMeshProUGUI myText;

    public void Setup(string text)
    {
        myRect = GetComponent<RectTransform>();
        myText = GetComponent<TextMeshProUGUI>();

        myText.text = text;

        transform.localScale = Vector3.one;

        myRect.DOAnchorPosY(myRect.anchoredPosition.y + 100f, 1f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }
}
