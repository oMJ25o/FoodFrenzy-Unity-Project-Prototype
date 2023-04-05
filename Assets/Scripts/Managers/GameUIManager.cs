using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private Text playerNameText;
    [SerializeField] private Text gameTimerText;
    [SerializeField] private Text countText;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject[] playerLives;

    public bool gameOver { get; private set; }

    private int gameTimer;
    private int currentLives;

    // Start is called before the first frame update
    void Start()
    {
        SetupGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BackToMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }

    private void SetupGame()
    {
        gameOver = false;
        foreach (var lives in playerLives)
        {
            lives.SetActive(true);
        }
        currentLives = playerLives.Length - 1;
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
            GameManager.gameInstance.CheckHighScore(int.Parse(countText.text.ToString()));
        }
    }

    public void DecreaseLife()
    {
        playerLives[currentLives].SetActive(false);
        if (currentLives == 0)
        {
            gameOver = true;
            ShowGameOverScreen();
        }
        currentLives--;
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