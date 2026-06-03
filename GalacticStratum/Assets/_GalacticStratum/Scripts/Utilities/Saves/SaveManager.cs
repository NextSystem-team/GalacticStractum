using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int moneyAmount;

    public int beskariumAmount;
    public int whitlockiteAmount;
    public int lechatelieriteAmount;
    public int elaliiteAmount;

    public List<String> toolsObtained = new();
}

public class GameData
{
    public int currentMoneyGoal;
    public int timeToReachGoal;
}

public class SettingsData
{
    public float musicVolume;
    public float sfxVolume;
}

public static class SaveManager
{
    private const string PLAYER_SAVE_KEY = "GalacticStractum_Player_SaveFile";
    private const string GAME_SAVE_KEY = "GalacticStractum_Game_SaveFile";
    private const string SETTINGS_SAVE_KEY = "GalacticStractum_Settings_SaveFile";
    public static PlayerData currentPlayerData;
    public static GameData currentGameData;
    public static SettingsData currentSettings;

    public static void LoadGame()
    {
        if (PlayerPrefs.HasKey(PLAYER_SAVE_KEY))
        {
            string playerJson = PlayerPrefs.GetString(PLAYER_SAVE_KEY);
            currentPlayerData = JsonUtility.FromJson<PlayerData>(playerJson);
        }
        else
        {
            currentPlayerData = new();
            currentPlayerData.toolsObtained.Add("tDrill"); 
        }

        if (PlayerPrefs.HasKey(GAME_SAVE_KEY))
        {
            string gameJson = PlayerPrefs.GetString(GAME_SAVE_KEY);
            currentGameData = JsonUtility.FromJson<GameData>(gameJson);
        }
        else
        {
            currentGameData = new()
            {
                currentMoneyGoal = 5000,
                timeToReachGoal = 3
            };
        }

        SaveGame();
    }

    public static void SaveGame()
    {
        string playerJson = JsonUtility.ToJson(currentPlayerData);
        PlayerPrefs.SetString(PLAYER_SAVE_KEY, playerJson);

        string gameJson = JsonUtility.ToJson(currentGameData);
        PlayerPrefs.SetString(GAME_SAVE_KEY, gameJson);

        PlayerPrefs.Save();
    }

    public static void LoadSettings()
    {
        if (PlayerPrefs.HasKey(SETTINGS_SAVE_KEY))
        {
            string settingsJson = PlayerPrefs.GetString(SETTINGS_SAVE_KEY);
            currentSettings = JsonUtility.FromJson<SettingsData>(settingsJson);
        }
        else
        {
            currentSettings = new() { 
                musicVolume = 0.5f,
                sfxVolume = 0.5f
            };
        }

        SaveSettings();
    }

    public static void SaveSettings()
    {
        string settingsJson = JsonUtility.ToJson(currentSettings);
        PlayerPrefs.SetString(SETTINGS_SAVE_KEY, settingsJson);
    }

    public static void ApplyAndSaveSettings(float musicVolume, float sfxVolume)
    {
        currentSettings.musicVolume = musicVolume;
        currentSettings.sfxVolume = sfxVolume;

        SaveSettings();
    }

    public static void ResetGame()
    {
        currentPlayerData = null;
        PlayerPrefs.DeleteKey(PLAYER_SAVE_KEY);
        currentGameData = null;
        PlayerPrefs.DeleteKey(GAME_SAVE_KEY);

        LoadGame();
    }

    public static bool CheckIfHasSavedGame()
    {
        return PlayerPrefs.HasKey(GAME_SAVE_KEY);
    }

    public static bool CheckIfHasTool(string toolId)
    {
        return currentPlayerData.toolsObtained.Contains(toolId);
    }

}
