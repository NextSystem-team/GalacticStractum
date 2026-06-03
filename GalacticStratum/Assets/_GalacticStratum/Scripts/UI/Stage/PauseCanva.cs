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

    private void Start()
    {
        GlobalEvents.NotifySettingsToggle += ToggleEscapeKey;

        openSettingsButton.onClick.AddListener(() => 
        { 
            GlobalEvents.ToggleSettings?.Invoke(); 
        });

        endMissionButton.onClick.AddListener(EndMission);
    }

    private void Update()
    {
        if (!isSettingsOpen && !isMissionEndingOpen && escape.action.WasPressedThisFrame())
        {
            Toggle();
        }
    }

    private void Toggle()
    {
        if (!isPaused)
        {
            Time.timeScale = 0.0f;
            isPaused = true;
            darkPanel.gameObject.SetActive(true);

            darkPanel.DOFade(0.8f, 0.3f).SetUpdate(true);
            pausePanel.DOAnchorPosY(0, 0.3f).SetUpdate(true);
        }
        else
        {
            Time.timeScale = 1.0f;
            isPaused = false;
            darkPanel.gameObject.SetActive(false);

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
