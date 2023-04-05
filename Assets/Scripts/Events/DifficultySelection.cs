using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelection : MonoBehaviour
{
    [SerializeField] private float difficultyValue;
    [SerializeField] private int difficultyTimer;
    [SerializeField] private int difficultyGravity;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void StartGame()
    {
        gameManager.StartGame(difficultyValue, difficultyTimer, difficultyGravity);
    }
}
