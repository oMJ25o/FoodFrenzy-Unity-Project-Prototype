using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Target : MonoBehaviour
{
    [SerializeField] protected float rotationSpeed;
    [SerializeField] protected float xRotation;
    [SerializeField] protected float yRotation;

    protected GameUIManager gameUIManager;
    protected AudioSource audioSource;
    protected GameObject collectionBox;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -120)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void SetupTarget()
    {
        gameUIManager = GameObject.Find("GameUI").GetComponent<GameUIManager>();
        audioSource = GetComponent<AudioSource>();
        RandomRotation();
    }
    protected virtual Vector3 GenerateRandomRotation() { return new Vector3(Random.Range(-xRotation, xRotation), Random.Range(-yRotation, yRotation)); }
    protected virtual void RandomRotation() { GetComponent<Rigidbody>().AddTorque(GenerateRandomRotation() * rotationSpeed, ForceMode.Impulse); }

    protected abstract void OnMouseDown();
}
