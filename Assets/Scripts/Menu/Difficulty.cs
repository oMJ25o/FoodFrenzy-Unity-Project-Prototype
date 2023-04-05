using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour
{
    [SerializeField] private int difficultyValue;
    [SerializeField] private int difficultyGravity;

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

    }

}