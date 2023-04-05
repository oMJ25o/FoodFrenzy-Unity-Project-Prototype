using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject newGameUI; //BASIC ENCAPSULATION
    [SerializeField] private GameObject mainMenuUI; //BASIC ENCAPSULATION
    // Start is called before the first frame update
    void Start()
    {

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

    public void BackToMainMenu()
    {
        newGameUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}