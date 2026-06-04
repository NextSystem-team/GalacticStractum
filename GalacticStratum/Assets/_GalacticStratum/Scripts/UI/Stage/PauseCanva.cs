using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseCanva : MonoBehaviour
{
    [SerializeField] private InputActionReference escape;

    [SerializeField] private Image darkPanel;
    [SerializeField] private RectTransform pausePanel;

    [SerializeField] private Button openSettingsButton;
    [SerializeField] private Button endMissionButton;

    [SerializeField] private EndMissionCanva endMissionCanva;

    private bool isPaused = false;
    private bool isSettingsOpen = false;
    private bool isMissionEndingOpen = false;

    private Texture2D previousCursorSprite;
    private Vector2 previousCursorHotspot;

    private void Start()
    {
        openSettingsButton.onClick.AddListener(() => 
        { 
            GlobalEvents.ToggleSettings?.Invoke(); 
        });

        endMissionButton.onClick.AddListener(EndMission);
    }

    private void OnEnable()
    {
        GlobalEvents.NotifySettingsToggle += ToggleEscapeKey;
    }

    private void OnDisable()
    {
        GlobalEvents.NotifySettingsToggle -= ToggleEscapeKey;
    }

    private void OnDestroy()
    {
        GlobalEvents.NotifySettingsToggle -= ToggleEscapeKey;
    }

    private void Update()
    {
        if (escape.action.WasPressedThisFrame())
        {
            if (isMissionEndingOpen || isSettingsOpen) return;

            Toggle();
        }
    }

    private void Toggle()
    {
        if (!isPaused)
        {
            Time.timeScale = 0.0f;
            isPaused = true;

            previousCursorSprite = CursorSpriteManager.currentCursorSprite;
            previousCursorHotspot = CursorSpriteManager.currentCursorHotspot;

            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            CursorSpriteManager.currentCursorSprite = null;
            CursorSpriteManager.currentCursorHotspot = Vector2.zero;

            darkPanel.gameObject.SetActive(true);

            darkPanel.DOFade(0.8f, 0.3f).SetUpdate(true);
            pausePanel.DOAnchorPosY(0, 0.3f).SetUpdate(true);
        }
        else
        {
            Time.timeScale = 1.0f;
            isPaused = false;
            darkPanel.gameObject.SetActive(false);

            Cursor.SetCursor(previousCursorSprite, previousCursorHotspot, CursorMode.Auto);
            CursorSpriteManager.currentCursorSprite = previousCursorSprite;
            CursorSpriteManager.currentCursorHotspot = previousCursorHotspot;

            darkPanel.DOFade(0f, 0.3f);
            pausePanel.DOAnchorPosY(-964, 0.3f).OnComplete(() =>
            {
                darkPanel.gameObject.SetActive(false);
            });
        }
    }

    private void ToggleEscapeKey(bool isSettingsOpen)
    {
        this.isSettingsOpen = isSettingsOpen;
    }

    private void EndMission()
    {
        isMissionEndingOpen = true;
        endMissionCanva.OpenEndMission();
    }
}
