using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ShopContainer : MonoBehaviour
{
    [SerializeField] private RectTransform shopBox;
    [SerializeField] private Button switchToBuyButton;
    [SerializeField] private Button switchToSellButton;

    private void Start()
    {
        switchToBuyButton.onClick.AddListener(SwitchToBuySession);
        switchToSellButton.onClick.AddListener(SwitchToSellSession);
    }

    private void SwitchToBuySession()
    {
        shopBox.DOAnchorPosX(0, 0.3f);
    }

    private void SwitchToSellSession()
    {
        shopBox.DOAnchorPosX(-946, 0.3f);
    }
}
