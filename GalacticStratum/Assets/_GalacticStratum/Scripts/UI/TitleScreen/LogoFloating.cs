using DG.Tweening;
using UnityEngine;

public class LogoFloating : MonoBehaviour
{
    [SerializeField] private float floatHeight;
    [SerializeField] private float cycleDuration;

    private RectTransform myRect;

    private void Start()
    {
        myRect = GetComponent<RectTransform>();

        myRect.DOAnchorPosY(myRect.anchoredPosition.y + floatHeight, cycleDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        transform.DORotate(new Vector3(0, 0, 2f), 2.3f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
