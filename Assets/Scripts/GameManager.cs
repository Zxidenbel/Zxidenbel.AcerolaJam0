using System.Collections.Generic;
using UnityEngine;
using Card;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static List<Character> Party;
    public static string LevelName;
    public static List<string> LevelsCompleted;
    public static int Progress;
    public static Options options = Options.Get();

    public static void OptionsReload()
    {
        options = Options.Get();
    }
    public static void SaveGameToFile()
    {
        GameSave save = new GameSave(true);
        string SaveData = JsonUtility.ToJson(save);
        File.WriteAllText(options.SavePath, SaveData);
    }
    public static void LoadGameFromFile()
    {
        Party.Clear();
        string SaveData = File.ReadAllText(options.SavePath);
        GameSave save = JsonUtility.FromJson<GameSave>(SaveData);
        foreach (CharacterSave characterSave in save.Party)
        {
            Character _character = new();
            _character.LoadSave(characterSave);
            Party.Add(_character);
        }
        LevelName = save.LevelName;
        Progress = save.Progress;
    }
    public static void NewGameInFile()
    {
        GameSave save = new GameSave(false);
        string SaveData = JsonUtility.ToJson(save);
        File.WriteAllText(options.SavePath, SaveData);
    }
    public static void StartGame()
    {
        SceneManager.LoadScene(LevelName);
    }
}

[Serializable]
public struct GameSave
{
    public CharacterSave[] Party;
    public string LevelName;
    public string[] LevelsCompleted;
    public int Progress;
    public GameSave(bool fromManager)
    {
        if (fromManager)
        {
            List<CharacterSave> _characterSaves = new();
            foreach (Character _character in GameManager.Party)
            {
                _characterSaves.Add(new CharacterSave(_character));
            }
            Party = _characterSaves.ToArray();
            LevelName = GameManager.LevelName;
            LevelsCompleted = GameManager.LevelsCompleted.ToArray();
            Progress = GameManager.Progress;
            return;
        }
        else
        {
            Party = new CharacterSave[0];
            LevelName = string.Empty;
            LevelsCompleted = new string[0];
            Progress = 0;
        }
    }
    public GameSave(string filePath)
    {
        string saveData = File.ReadAllText(filePath);
        this = JsonUtility.FromJson<GameSave>(saveData);
    }
}
