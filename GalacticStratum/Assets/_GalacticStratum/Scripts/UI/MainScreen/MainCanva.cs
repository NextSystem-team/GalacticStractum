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

    [SerializeField] private TextMeshProUGUI currentQuotaText;
    [SerializeField] private TextMeshProUGUI timeToReachGoalText;
    [SerializeField] private TextMeshProUGUI moneyAmountText;
    private int moneyAmount = -1;

    [SerializeField] private GameObject quotaReachedPanel;
    [SerializeField] private TextMeshProUGUI quotaChangeText;

    [SerializeField] private GameObject quotaFailedPanel;

    [SerializeField] private Button quotaReachedButton;
    [SerializeField] private Button quotaFailedButton;

    private void Start()
    {
        if (AudioManager.Instance.MusicSource.clip != AudioManager.Instance.GetMusic("MainMusic"))
        {
            AudioManager.Instance.PlayMusic("MainMusic");
        }

        startGameButton.onClick.AddListener(StartGame);
        openShopButton.onClick.AddListener(ToggleShop);
        backToTitleButton.onClick.AddListener(BackToTitle);

        quotaReachedButton.onClick.AddListener(CloseQuotaReachedPanel);
        quotaFailedButton.onClick.AddListener(() =>
        {
            SaveManager.ResetGame();
            DOTween.KillAll();
            SceneManager.LoadScene("TitleScreenScene");
        });

        if (escape != null && escape.action != null)
        {
            escape.action.Enable();
        }

        currentQuotaText.text = "$" + SaveManager.currentGameData.currentMoneyQuota.ToString("N0", CultureInfo.CurrentCulture);
        timeToReachGoalText.text = $"{SaveManager.currentGameData.timeToReachQuota} Years";

        if (SaveManager.currentGameData.timeToReachQuota <= 0)
        {
            if (SaveManager.currentPlayerData.moneyAmount >= SaveManager.currentGameData.currentMoneyQuota)
            {
                string currentQuota = SaveManager.currentGameData.currentMoneyQuota.ToString("N0", CultureInfo.CurrentCulture);
                string nextQuota = (SaveManager.currentGameData.currentMoneyQuota + 60000).ToString("N0", CultureInfo.CurrentCulture);

                quotaChangeText.text = $"{currentQuota} > {nextQuota}";

                SaveManager.currentGameData.currentMoneyQuota += 60000;
                SaveManager.currentGameData.timeToReachQuota = 3;

                OpenQuotaReachedPanel();
            }
            else
            {
                OpenQuotaFailedPanel();
            }
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
            ToggleShop();
        }
    }

    private void StartGame()
    {
        clickBlockingPanel.SetActive(true);
        playerImage.DOAnchorPosY(1800, 4.5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            DOTween.KillAll();
            SaveManager.SaveGame();
            ResourcesPriceManager.UpdateResourcesPrices();
            SaveManager.currentGameData.timeToReachQuota--;
            SceneManager.LoadScene("GameplayScene");
        });
    }

    private void ToggleShop()
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

    private void OpenQuotaReachedPanel()
    {
        quotaReachedPanel.SetActive(true);

        quotaReachedPanel.GetComponent<CanvasGroup>().DOFade(1, 1f);
    }

    private void CloseQuotaReachedPanel()
    {
        quotaReachedPanel.GetComponent<CanvasGroup>().DOFade(0, 0.3f).OnComplete(() =>
        {
            quotaReachedPanel.SetActive(false);
        });
    }

    private void OpenQuotaFailedPanel()
    {
        quotaFailedPanel.SetActive(true);
        quotaFailedPanel.GetComponent<CanvasGroup>().DOFade(1, 1f);
    }

    private void BackToTitle()
    {
        SaveManager.SaveGame();
        DOTween.KillAll();
        SceneManager.LoadScene("TitleScreenScene");
    }
}
