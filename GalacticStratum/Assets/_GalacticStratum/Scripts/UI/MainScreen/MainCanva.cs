using DG.Tweening;
using System.Globalization;
using TMPro;
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
    [SerializeField] private GameObject clickBlockingPanel;

    [SerializeField] private TextMeshProUGUI moneyAmountText;
    private int moneyAmount;

    private void Start()
    {
        startGameButton.onClick.AddListener(StartGame);
        openShopButton.onClick.AddListener(TogleShop);
        backToTitleButton.onClick.AddListener(BackToTitle);

        if (escape != null && escape.action != null)
        {
            escape.action.Enable();
        }
    }

    private void OnEnable()
    {
        escape.action.Enable();
    }

    private void OnDisable()
    {
        escape.action.Disable();
    }

    private void Update()
    {
        if (moneyAmount != SaveManager.currentPlayerData.moneyAmount)
        {
            moneyAmount = SaveManager.currentPlayerData.moneyAmount;
            moneyAmountText.text = "$" + moneyAmount.ToString("N0", CultureInfo.CurrentCulture);
        }

        if (isShopOpened && escape.action.WasPressedThisFrame())
        {
            TogleShop();
        }
    }

    private void StartGame()
    {
        clickBlockingPanel.SetActive(true);
        playerImage.DOAnchorPosY(1800, 4.5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            DOTween.KillAll();
            ResourcesPriceManager.UpdateResourcesPrices();
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
        DOTween.KillAll();
        SceneManager.LoadScene("TitleScreenScene");
    }
}
