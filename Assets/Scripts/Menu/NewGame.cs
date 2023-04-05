using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewGame : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerNameInputField;
    // Start is called before the first frame update
    void Start()
    {
        playerNameInputField.onValueChanged.AddListener(SetPlayerName);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetPlayerName(string name)
    {
        GameManager.gameInstance.playerName = name;
    }

}
