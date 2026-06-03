using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenCanva : MonoBehaviour
{
    [SerializeField] private Button loadGame;
    [SerializeField] private Button startNewGame;
    [SerializeField] private Button openSettings;
    [SerializeField] private Button exitGame;

    void Start()
    {
        loadGame.onClick.AddListener(LoadCampaign);
        startNewGame.onClick.AddListener(StartNewCampaign);
        openSettings.onClick.AddListener(OpenSettings);
        exitGame.onClick.AddListener(ExitGame);

        if (!SaveManager.CheckIfHasSavedGame())
        {
            loadGame.gameObject.SetActive(false);
        }
    }

    private void LoadCampaign()
    {
        SaveManager.LoadGame();
        SceneManager.LoadScene("MainScene");
    }

    private void StartNewCampaign()
    {
        SaveManager.ResetGame();
        SceneManager.LoadScene("MainScene");
    }

    private void OpenSettings()
    {
        GlobalEvents.ToggleSettings?.Invoke();
    }

    private void ExitGame() 
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
