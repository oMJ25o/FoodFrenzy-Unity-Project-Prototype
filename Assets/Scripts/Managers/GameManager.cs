using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

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

    void Start()
    {
        if (gameInstance == null)
        {
            gameInstance = this;
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

    public void CheckHighScore(int score)
    {
        string path = Application.persistentDataPath + "/highscore_savefile.json";

        if (!File.Exists(path))
        {
            GameManager.gameInstance.scoreData.highScoreData.Add(playerName, score);
            SaveHighScoreFile(path);
        }
        else
        {
            HighScoreData existingHighScore = new HighScoreData();

            string json = File.ReadAllText(path);
            existingHighScore = JsonUtility.FromJson<HighScoreData>(json);

            if (!existingHighScore.highScoreData.ContainsKey(playerName))
            {
                GameManager.gameInstance.scoreData.highScoreData.Add(playerName, score);
                SaveHighScoreFile(path);
            }
            else if (existingHighScore.highScoreData.ContainsKey(playerName) && score > existingHighScore.highScoreData[playerName])
            {
                existingHighScore.highScoreData[playerName] = score;
                GameManager.gameInstance.scoreData.highScoreData = existingHighScore.highScoreData;
                SaveHighScoreFile(path);
            }
        }
    }

    private void SaveHighScoreFile(string path)
    {
        string json = JsonUtility.ToJson(GameManager.gameInstance.scoreData);
        File.WriteAllText(path, json);
    }

    [System.Serializable]
    class HighScoreData
    {
        public Dictionary<string, int> highScoreData; //Dictionary of high score per player name ex. ["MJ", 25]
    }
}
