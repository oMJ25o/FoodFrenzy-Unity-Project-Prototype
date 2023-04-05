using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float xRotation;
    [SerializeField] private float yRotation;
    [SerializeField] private AudioClip explosionAudio;
    [SerializeField] private AudioClip catchAudio;

    private AudioSource audioSource;
    private GameManager gameManager;
    private ParticleSystem bombExplosion;
    private GameObject collectionBox;
    // Start is called before the first frame update
    void Start()
    {
        collectionBox = GameObject.Find("CollectionBox");
        bombExplosion = GameObject.Find("BombExplosion").GetComponent<ParticleSystem>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        RandomRotation();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -120)
        {
            Destroy(gameObject);
        }
    }
    Vector3 GenerateRandomRotation() { return new Vector3(Random.Range(-xRotation, xRotation), Random.Range(-yRotation, yRotation)); }
    private void RandomRotation()
    {
        GetComponent<Rigidbody>().AddTorque(GenerateRandomRotation() * rotationSpeed, ForceMode.Impulse);
    }

    private void OnMouseDown()
    {
        if (gameObject.CompareTag("Food") && !gameManager.gameOver)
        {
            transform.position = new Vector3(collectionBox.transform.position.x, 10, -10f);
            transform.localScale = new Vector3(10, 10, 10);
            audioSource.PlayOneShot(catchAudio);
        }
        else if (gameObject.CompareTag("Bomb") && !gameManager.gameOver)
        {
            bombExplosion.Play();
            gameManager.DecreaseLife();
            bombExplosion.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }

    }
}