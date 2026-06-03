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

    public void OpenEndMission()
    {
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
        SceneManager.LoadScene("MainScene");
    }
}
