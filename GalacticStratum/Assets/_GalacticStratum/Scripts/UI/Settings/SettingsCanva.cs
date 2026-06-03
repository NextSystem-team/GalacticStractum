using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SettingsCanva : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Button applySettings;

    [SerializeField] private Image darkPanel;
    [SerializeField] private RectTransform settingsPanel;

    [SerializeField] private InputActionReference escape;

    private bool isOpened = false;

    public static SettingsCanva Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SaveManager.LoadSettings();

        UpdateVolumes();

        applySettings.onClick.AddListener(ApplySettings);

        GlobalEvents.ToggleSettings += Toggle;
    }

    private void Update()
    {
        if (isOpened && escape.action.WasPressedThisFrame())
        {
            Toggle();
        }
    }

    private void Toggle()   
    {
        if (!isOpened)
        {
            UpdateVolumes();

            isOpened = true;
            darkPanel.gameObject.SetActive(true);

            darkPanel.DOFade(0.8f, 1f);
            settingsPanel.DOAnchorPosY(0, 1f);
        }
        else
        {
            UpdateVolumes();

            isOpened = false;
            darkPanel.DOFade(0.0f, 1f);
            settingsPanel.DOAnchorPosY(-964, 1f).OnComplete(() =>
            {
                darkPanel.gameObject.SetActive(false);
            });
        }
    }

    private void ApplySettings()
    {
        SaveManager.ApplyAndSaveSettings(musicSlider.value, sfxSlider.value);

        Toggle();
    }

    private void UpdateVolumes()
    {
        musicSlider.value = SaveManager.currentSettings != null ? SaveManager.currentSettings.musicVolume : 0.5f;
        sfxSlider.value = SaveManager.currentSettings != null ? SaveManager.currentSettings.sfxVolume : 0.5f;
    }
}
