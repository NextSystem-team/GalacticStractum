using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMissionCanva : MonoBehaviour
{
    [SerializeField] private GameObject darkPanel;
    [SerializeField] private Button backToHomeButton;

    private CanvasGroup group;

    private void Start()
    {
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

        darkPanel.SetActive(true);
        Time.timeScale = 0.0f;

        if (group != null)
        {
            group.DOFade(1, 1f).SetUpdate(true);
        }
    }

    private void BackToHome()
    {
        Time.timeScale = 1.0f;
        DOTween.KillAll();
        SaveManager.SaveGame();
        SceneManager.LoadScene("MainScene");
    }
}
