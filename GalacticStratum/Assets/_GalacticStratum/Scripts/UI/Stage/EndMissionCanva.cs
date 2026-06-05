using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMissionCanva : MonoBehaviour
{
    [SerializeField] private float maxMoneyBonusForDiscoveredMap = 30000f;

    [SerializeField] private GameObject darkPanel;
    [SerializeField] private Button backToHomeButton;

    [SerializeField] private TextMeshProUGUI lechatelieriteCollectedText;
    [SerializeField] private TextMeshProUGUI elaliiteCollectedText;
    [SerializeField] private TextMeshProUGUI beskariumCollectedText;
    [SerializeField] private TextMeshProUGUI whitlockiteCollectedText;

    [SerializeField] private TextMeshProUGUI mapDiscoveredPercentage;
    [SerializeField] private TextMeshProUGUI moneyBonusText;

    [SerializeField] private RenderTexture persistentMapRT;

    private PlayerStorage playerStorage;

    private CanvasGroup group;

    private void Start()
    {
        playerStorage = FindFirstObjectByType<PlayerStorage>();
        group = GetComponent<CanvasGroup>();

        backToHomeButton.onClick.AddListener(BackToHome);
    }

    private void OnEnable()
    {
        GlobalEvents.EndMission += OpenEndMission;
    }

    private void OnDisable()
    {
        GlobalEvents.EndMission -= OpenEndMission;
    }

    private void OnDestroy()
    {
        GlobalEvents.EndMission -= OpenEndMission;
    }

    public void OpenEndMission()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        CursorSpriteManager.currentCursorSprite = null;
        CursorSpriteManager.currentCursorHotspot = Vector2.zero;

        float discoveryPercentage = CalculateFinalDiscoveryPercentage();
        mapDiscoveredPercentage.text = $"{discoveryPercentage:0}%";

        int moneyBonus = Mathf.RoundToInt((discoveryPercentage / 100f) * maxMoneyBonusForDiscoveredMap);
        moneyBonusText.text = $"Discover Bonus: +${moneyBonus:N0}";
        SaveManager.currentPlayerData.moneyAmount += moneyBonus;

        lechatelieriteCollectedText.text = $"+{playerStorage.lechatelieriteAmount}";
        elaliiteCollectedText.text = $"+{playerStorage.elaliiteAmount}";
        beskariumCollectedText.text = $"+{playerStorage.beskariumAmount}";
        whitlockiteCollectedText.text = $"+{playerStorage.whitlockiteAmount}";

        darkPanel.SetActive(true);
        Time.timeScale = 0.0f;

        if (group != null)
        {
            group.DOFade(1, 1f).SetUpdate(true);
        }
    }

    private float CalculateFinalDiscoveryPercentage()
    {
        Texture2D tempTexture = new(persistentMapRT.width, persistentMapRT.height, TextureFormat.RGBA32, false);

        RenderTexture.active = persistentMapRT;
        tempTexture.ReadPixels(new Rect(0, 0, persistentMapRT.width, persistentMapRT.height), 0, 0);
        tempTexture.Apply();
        RenderTexture.active = null;

        Color32[] pixels = tempTexture.GetPixels32();

        int totalPixels = pixels.Length;
        int discoveredPixels = 0;

        for (int i = 0; i < totalPixels; i++)
        {
            if (pixels[i].r > 0)
            {
                discoveredPixels++;
            }
        }

        Destroy(tempTexture);

        return ((float)discoveredPixels / totalPixels) * 100f;
    }

    private void BackToHome()
    {
        Time.timeScale = 1.0f;
        DOTween.KillAll();
        SaveManager.SaveGame();
        SceneManager.LoadScene("MainScene");
    }
}
