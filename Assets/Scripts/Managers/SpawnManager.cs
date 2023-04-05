using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameUIManager gameUIManager;
    [SerializeField] GameObject[] targetPrefabs; //Arrays of target prefabs to spawn

    private float gravityStandard = 9.81f;
    private float xRange = 60;
    private float yRange = 120;
    private float zRange = 10;
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, (-gravityStandard * GameManager.gameInstance.difficultyGravity), 0);
        Invoke("SpawnTargets", GameManager.gameInstance.difficultyValue);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Spawn random targets in randown x spawn location
    private void SpawnTargets()
    {
        if (!gameUIManager.gameOver)
        {
            int index = Random.Range(0, targetPrefabs.Length);
            Instantiate(targetPrefabs[index], GenerateRandomSpawnLocation(), targetPrefabs[index].transform.rotation);
            Invoke("SpawnTargets", GameManager.gameInstance.difficultyValue);
        }
    }

    //ABSTRACTION
    //Generate random spawn locations for spawning targets
    private Vector3 GenerateRandomSpawnLocation() { return new Vector3(Random.Range(-xRange, xRange), yRange, -zRange); }
}