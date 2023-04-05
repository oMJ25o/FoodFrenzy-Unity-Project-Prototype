using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour
{
    [SerializeField] private float difficultyValue;
    [SerializeField] private int difficultyGravity;
    [SerializeField] private int difficultyTimer;

    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetDifficulty()
    {
        GameManager.gameInstance.difficultyValue = difficultyValue;
        GameManager.gameInstance.difficultyTimer = difficultyTimer;
        GameManager.gameInstance.difficultyGravity = difficultyGravity;

        GameManager.gameInstance.StartGame();
    }

}
