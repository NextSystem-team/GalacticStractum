using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int moneyAmount;

    public int beskariumAmount;
    public int merriliteAmount;
    public int lechatelieriteAmount;
    public int elaliiteAmount;

    public List<String> toolsObtained;
}

public class GameData
{
    public float musicVolume;
    public float sfxVolume;

    public int currentMoneyGoal;
}

public static class SaveManager
{
    private const string PLAYER_SAVE_KEY = "GalacticStractum_Player_SaveFile";
    private const string GAME_SAVE_KEY = "GalacticStractum_Game_SaveFile";
    public static PlayerData currentPlayerData = new();
    public static GameData currentGameData = new();

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
                musicVolume = 0.5f,
                sfxVolume = 0.5f,
                currentMoneyGoal = 5000
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

    public static bool CheckIfHasTool(string toolId)
    {
        return currentPlayerData.toolsObtained.Contains(toolId);
    }

}
