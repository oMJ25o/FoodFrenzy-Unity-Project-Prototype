using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private Text playerNameText;
    [SerializeField] private Text gameTimerText;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject[] playerLives;

    public bool gameOver { get; private set; }

    private int gameTimer;

    // Start is called before the first frame update
    void Start()
    {
        SetupGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetupGame()
    {
        gameOver = false;
        playerNameText.text = "Player: " + GameManager.gameInstance.playerName;
        gameTimer = GameManager.gameInstance.difficultyTimer;
        gameTimerText.text = "Timer: " + gameTimer;
        StartCoroutine("StartGameTimer");
    }

    private void ShowGameOverScreen()
    {
        if (gameOver)
        {
            gameOverScreen.SetActive(true);
        }
    }

    IEnumerator StartGameTimer()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(1);
            gameTimer--;
            gameTimerText.text = "Timer: " + gameTimer;

            if (gameTimer == 0)
            {
                gameOver = true;
                ShowGameOverScreen();
            }
        }
    }

}