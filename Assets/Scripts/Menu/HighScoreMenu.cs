using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class HighScoreMenu : MainMenu
{
    [SerializeField] protected GameObject highScoreMenuUI;
    [SerializeField] private GameObject[] playerHighScoreNameData;
    [SerializeField] private GameObject[] playerHighScoreData;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenHighScoreUI()
    {
        mainMenuUI.SetActive(false);
        highScoreMenuUI.SetActive(true);
        GameManager.gameInstance.GetAllHighScoreData();
        DisplayAllHighScore();
    }

    public void DisplayAllHighScore()
    {
        Dictionary<string, int> currentHighScoreData = new Dictionary<string, int>();

        for (int i = 0; i < GameManager.gameInstance.currentHighScorePlayerName.Count; i++)
        {
            currentHighScoreData.Add(GameManager.gameInstance.currentHighScorePlayerName[i], GameManager.gameInstance.currentHighScoreData[i]);
        }

        int index = 0;

        foreach (var item in currentHighScoreData.OrderByDescending(r => r.Value))
        {
            playerHighScoreData[index].SetActive(true);
            playerHighScoreNameData[index].SetActive(true);
            playerHighScoreNameData[index].GetComponentInChildren<TMP_Text>().text = item.Key;
            playerHighScoreData[index].GetComponentInChildren<TMP_Text>().text = item.Value.ToString();
            index++;
        }
    }

    public override void BackToMainMenu()
    {
        highScoreMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
}
