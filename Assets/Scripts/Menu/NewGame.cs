using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewGame : MainMenu
{
    [SerializeField] private TMP_InputField playerNameInputField;
    [SerializeField] protected GameObject newGameUI;
    //BASIC ENCAPSULATION
    // Start is called before the first frame update
    void Start()
    {
        playerNameInputField.onValueChanged.AddListener(SetPlayerName);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenNewGame()
    {
        mainMenuUI.SetActive(false);
        newGameUI.SetActive(true);
    }

    public override void BackToMainMenu()
    {
        newGameUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    private void SetPlayerName(string name)
    {
        GameManager.gameInstance.playerName = name;
    }

}
