using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainCanva : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private RectTransform playerImage;

    [SerializeField] private Button openShopButton;
    [SerializeField] private Image darkShopPanel;
    [SerializeField] private RectTransform shopPanel;
    [SerializeField] private InputActionReference escape;
    private bool isShopOpened;

    [SerializeField] private Button backToTitleButton;

    private void Start()
    {
        startGameButton.onClick.AddListener(StartGame);
        openShopButton.onClick.AddListener(TogleShop);
        backToTitleButton.onClick.AddListener(BackToTitle);
    }

    private void Update()
    {
        if (isShopOpened && escape.action.WasPressedThisFrame())
        {
            TogleShop();
        }
    }

    private void StartGame()
    {
        playerImage.DOAnchorPosY(1800, 4.5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            SceneManager.LoadScene("GameplayScene");
        });
    }

    private void TogleShop()
    {
        if (!isShopOpened)
        {
            isShopOpened = true;

            darkShopPanel.gameObject.SetActive(true);

            darkShopPanel.DOFade(0.8f, 0.3f);
            shopPanel.DOAnchorPosY(0, 0.3f);
        }
        else
        {
            isShopOpened = false;

            darkShopPanel.DOFade(0.0f, 0.3f);
            shopPanel.DOAnchorPosY(-964, 0.3f).OnComplete(() =>
            {
                darkShopPanel.gameObject.SetActive(false);
            });
        }
    }

    private void BackToTitle()
    {
        SaveManager.SaveGame();
        SceneManager.LoadScene("TitleScreenScene");
    }
}
