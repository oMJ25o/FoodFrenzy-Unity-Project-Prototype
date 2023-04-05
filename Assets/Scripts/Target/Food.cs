using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Target
{
    [SerializeField] protected AudioClip catchAudio;
    private Vector3 newScale = new Vector3(10, 10, 10);
    // Start is called before the first frame update
    void Start()
    {
        SetupTarget();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void SetupTarget()
    {
        base.SetupTarget();
        collectionBox = GameObject.Find("CollectionBox");
    }

    protected override void OnMouseDown()
    {
        if (!gameUIManager.gameOver)
        {
            transform.position = new Vector3(collectionBox.transform.position.x, 10, -10f);
            transform.localScale = newScale;
            audioSource.PlayOneShot(catchAudio);
        }
    }
}
