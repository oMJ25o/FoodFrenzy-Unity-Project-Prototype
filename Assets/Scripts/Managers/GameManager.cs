using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private string b_playerName; //BACKING PLAYER NAME VARIABLE
    private int b_difficultyGravity; //BACKING DIFFICULTY GRAVITY VARIABLE
    private int b_difficultyTimer; //BACKING DIFFICULTY TIMER VARIABLE
    private float b_DifficultyValue; //BACKING DIFFICULTY VALUE VARIABLE

    public string playerName //ENCAPSULATION AUTO-IMPLEMENTED PROPERTY
    {
        get { return b_playerName; }
        set { b_playerName = value; }
    }
    public int difficultyGravity //ENCAPSULATION AUTO-IMPLEMENTED PROPERTY
    {
        get { return b_difficultyGravity; }
        set
        {
            if (value > 0)
            {
                b_difficultyGravity = value;
            }
            else
            {
                Debug.Log("Difficulty gravity cannot be negative");
            }
        }
    }
    public int difficultyTimer //ENCAPSULATION AUTO-IMPLEMENTED PROPERTY
    {
        get { return b_difficultyTimer; }
        set
        {
            if (value > 0.0f)
            {
                b_difficultyTimer = value;
            }
            else
            {
                Debug.Log("Difficulty timer cannot be negative or 0");
            }
        }
    }
    public float difficultyValue //ENCAPSULATION AUTO-IMPLEMENTED PROPERTY
    {
        //Return the value in the Backing Variable of Difficulty Value
        get { return b_DifficultyValue; }

        //Set the value into the backing variable while making sure it wont set into negative or 0
        set
        {
            if (value > 0.0f)
            {
                b_DifficultyValue = value;
            }
            else
            {
                Debug.Log("Difficulty Value cannot be a negative or 0 value.");
            }
        }
    }
    public static GameManager gameInstance { get; private set; } //ENCAPSULATION AUTO-IMPLEMENTED PROPERTY
    private HighScoreData scoreData;

    [HideInInspector] public Dictionary<string, int> currentHighScoreDataDict;
    [HideInInspector] public List<int> currentHighScoreData;
    [HideInInspector] public List<string> currentHighScorePlayerName;

    void Start()
    {
        if (gameInstance == null)
        {
            gameInstance = this;
            GameManager.gameInstance.scoreData = new HighScoreData();
            GameManager.gameInstance.scoreData.highScorePlayerData = new List<int>();
            GameManager.gameInstance.scoreData.highScorePlayerName = new List<string>();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GetAllHighScoreData()
    {
        string path = Application.persistentDataPath + "/highscore_savefile.json";
        if (File.Exists(path))
        {
            GetSaveFileData(path);
            currentHighScoreData = GameManager.gameInstance.scoreData.highScorePlayerData;
            currentHighScorePlayerName = GameManager.gameInstance.scoreData.highScorePlayerName;
        }

    }

    public void GetSaveFileData(string path)
    {
        string json = File.ReadAllText(path);
        GameManager.gameInstance.scoreData = JsonUtility.FromJson<HighScoreData>(json);
    }

    public void CheckHighScore(int score)
    {
        string path = Application.persistentDataPath + "/highscore_savefile.json";

        if (!File.Exists(path))
        {
            AddDataToHighScoreData(score, path);
        }
        else
        {
            HighScoreData existingHighScore = new HighScoreData();
            existingHighScore.highScorePlayerData = new List<int>();
            existingHighScore.highScorePlayerName = new List<string>();

            string json = File.ReadAllText(path);
            existingHighScore = JsonUtility.FromJson<HighScoreData>(json);

            GetSaveFileData(path);
            for (int i = 0; i < existingHighScore.highScorePlayerName.Count; i++)
            {
                if (existingHighScore.highScorePlayerName[i] == playerName && score > existingHighScore.highScorePlayerData[i])
                {
                    GameManager.gameInstance.scoreData.highScorePlayerData[i] = score;
                    SaveHighScoreFile(path);
                    return;
                }
                else if (existingHighScore.highScorePlayerName[i] != playerName && i == (existingHighScore.highScorePlayerName.Count - 1) && existingHighScore.highScorePlayerName.Count < 5)
                {
                    AddDataToHighScoreData(score, path);
                    return;
                }
                else if (score > existingHighScore.highScorePlayerData[i] && existingHighScore.highScorePlayerName.Count >= 5)
                {
                    MoveToDictionary();
                    GameManager.gameInstance.currentHighScoreDataDict.Remove(currentHighScoreDataDict.OrderByDescending(r => r.Value).Last().Key);
                    MoveToHighScoreData();
                    AddDataToHighScoreData(score, path);
                    return;
                }
            }
        }
    }

    public void MoveToDictionary()
    {
        GameManager.gameInstance.currentHighScoreDataDict = new Dictionary<string, int>();
        for (int x = 0; x < GameManager.gameInstance.currentHighScorePlayerName.Count; x++)
        {
            GameManager.gameInstance.currentHighScoreDataDict.Add(GameManager.gameInstance.currentHighScorePlayerName[x], GameManager.gameInstance.currentHighScoreData[x]);
        }
    }

    public void MoveToHighScoreData()
    {
        int i = 0;

        GameManager.gameInstance.scoreData.highScorePlayerData = new List<int>();
        GameManager.gameInstance.scoreData.highScorePlayerName = new List<string>();
        foreach (var item in currentHighScoreDataDict)
        {
            GameManager.gameInstance.scoreData.highScorePlayerName.Add(item.Key);
            GameManager.gameInstance.scoreData.highScorePlayerData.Add(item.Value);
            i++;
        }
    }

    private void AddDataToHighScoreData(int score, string path)
    {
        GameManager.gameInstance.scoreData.highScorePlayerName.Add(playerName);
        GameManager.gameInstance.scoreData.highScorePlayerData.Add(score);
        SaveHighScoreFile(path);
    }

    private void SaveHighScoreFile(string path)
    {
        string json = JsonUtility.ToJson(GameManager.gameInstance.scoreData);
        File.WriteAllText(path, json);
    }

    [System.Serializable]
    class HighScoreData
    {
        public List<string> highScorePlayerName;
        public List<int> highScorePlayerData;
    }
}
