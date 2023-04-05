using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Target
{
    [SerializeField] protected AudioClip explosionAudio;
    protected ParticleSystem bombExplosion;
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
        bombExplosion = GameObject.Find("BombExplosion").GetComponent<ParticleSystem>();
    }

    protected override void OnMouseDown()
    {
        if (!gameUIManager.gameOver)
        {
            bombExplosion.Play();
            //gameManager.DecreaseLife();
            bombExplosion.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }
}
