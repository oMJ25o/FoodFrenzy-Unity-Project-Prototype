using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] playerLives;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject countText;
    [SerializeField] private GameObject[] targetPrefabs;
    [SerializeField] private Text timerText;
    [SerializeField] private float xRange;
    [SerializeField] private float yRange;
    [SerializeField] private float gravityModifier;
    public bool gameOver = false;

    private int currentLives;
    private int maxTimer;
    private int timer;
    private float difficultyValue;

    public void StartGame(float diffValue, int difficultyTimer, int difficultyGravity)
    {
        titleScreen.gameObject.SetActive(false);
        timerText.gameObject.SetActive(true);
        countText.gameObject.SetActive(true);
        currentLives = playerLives.Length - 1;
        for (int i = 0; i < playerLives.Length; i++)
        {
            playerLives[i].gameObject.SetActive(true);
        }

        difficultyValue = diffValue;
        maxTimer = difficultyTimer;

        Physics.gravity = new Vector3(0, (-9.81f * (difficultyGravity + gravityModifier)), 0);
        timerText.text = "Timer: " + maxTimer;
        SpawnFood();
        StartCoroutine("GameTimer");
    }

    Vector3 GenerateSpawnLocation() { return new Vector3(Random.Range(-xRange, xRange), yRange, -10); }

    private void SpawnFood()
    {
        if (!gameOver)
        {
            int index = Random.Range(0, targetPrefabs.Length);
            Instantiate(targetPrefabs[index], GenerateSpawnLocation(), targetPrefabs[index].transform.rotation);
            Invoke("SpawnFood", difficultyValue);
        }
    }

    IEnumerator GameTimer()
    {
        timer = 0;
        while (!gameOver)
        {
            yield return new WaitForSeconds(1);
            timer++;
            timerText.text = "Timer: " + (maxTimer - timer);
            if (timer == maxTimer)
            {
                gameOver = true;
                ShowGameOverScreen();
            }
        }
    }

    public void DecreaseLife()
    {
        Destroy(playerLives[currentLives].gameObject);
        currentLives--;
        if (currentLives < 0)
        {
            gameOver = true;
            ShowGameOverScreen();
        }
    }

    void ShowGameOverScreen() { gameOverScreen.gameObject.SetActive(true); }

}
