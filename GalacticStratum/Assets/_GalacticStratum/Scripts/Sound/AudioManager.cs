using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private AudioData[] gameSFXs;
    private AudioData[] gameMusics;
    private Dictionary<string, AudioData> sfxList = new();
    private Dictionary<string, AudioData> musicList = new();

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    public AudioSource MusicSource => musicSource; 
    public AudioSource SFXSource => sfxSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        gameMusics = Resources.LoadAll<AudioData>("Musics");
        gameSFXs = Resources.LoadAll<AudioData>("Sounds");

        foreach (AudioData sound in gameMusics)
        {
            musicList.Add(sound.soundName, sound);
        }

        foreach (AudioData sound in gameSFXs)
        {
            sfxList.Add(sound.soundName, sound);
        }
    }

    public void PlayMusic(string soundName)
    {
        if (musicList.TryGetValue(soundName, out AudioData value))
        {
            musicSource.clip = value.soundClip;
            musicSource.Play();
        }
        else
        {
            print("Não achei o som");
        }
    }

    public void PlaySFX(string soundName)
    {
        if (sfxList.TryGetValue(soundName, out AudioData value))
        {
            AudioData audio = value;

            sfxSource.pitch = Random.Range(0.95f, 1.05f);

            sfxSource.PlayOneShot(audio.soundClip);
        }
        else
        {
            print("Não achei o som");
        }
    }

    public AudioClip GetSound(string soundName)
    {
        AudioClip sound = null;

        if (sfxList.TryGetValue(soundName, out AudioData value))
        {
            sound = value.soundClip;
        }

        return sound;
    }

    public AudioClip GetMusic(string soundName)
    {
        AudioClip sound = null;

        if (musicList.TryGetValue(soundName, out AudioData value))
        {
            sound = value.soundClip;
        }

        return sound;
    }
}
