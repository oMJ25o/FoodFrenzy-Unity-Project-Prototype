using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gameInstance { get; private set; } //ENCAPSULATION
    private string b_playerName; //BACKING VARIABLE
    private int b_DifficultyValue; //BACKING VARIABLE
    public int difficultyValue //ENCAPSULATION
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
    public string playerName //ENCAPSULATION
    {
        get { return b_playerName; }
        set { b_playerName = value; }
    }

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
        Debug.Log(playerName);
        Debug.Log(difficultyValue);
    }
}
