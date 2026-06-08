using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsCanva : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

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

        musicSlider.onValueChanged.AddListener((value) =>
        {
            audioMixer.SetFloat("MusicVolume", value);
        });

        sfxSlider.onValueChanged.AddListener((value) =>
        {
            audioMixer.SetFloat("SFXVolume", value);
        });
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        GlobalEvents.ToggleSettings += Toggle;

        ActivateInput();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        GlobalEvents.ToggleSettings -= Toggle;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ActivateInput();
    }

    private void ActivateInput()
    {
        if (escape != null && escape.action != null)
        {
            escape.action.Enable();
        }
    }

    private void LateUpdate()
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

            GlobalEvents.NotifySettingsToggle?.Invoke(true);
            isOpened = true;
            darkPanel.gameObject.SetActive(true);

            darkPanel.DOFade(0.8f, 0.3f).SetUpdate(true);
            settingsPanel.DOAnchorPosY(0, 0.3f).SetUpdate(true);
        }
        else
        {
            UpdateVolumes();

            AudioManager.Instance.PlaySFX("Sweep");

            GlobalEvents.NotifySettingsToggle?.Invoke(false);
            isOpened = false;
            darkPanel.DOFade(0.0f, 0.3f).SetUpdate(true);
            settingsPanel.DOAnchorPosY(-964, 0.3f).SetUpdate(true).OnComplete(() =>
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
        musicSlider.value = SaveManager.currentSettings != null ? SaveManager.currentSettings.musicVolume : -30f;
        audioMixer.SetFloat("MusicVolume", musicSlider.value);
        sfxSlider.value = SaveManager.currentSettings != null ? SaveManager.currentSettings.sfxVolume : -30f;
        audioMixer.SetFloat("SFXVolume", sfxSlider.value);
    }
}
