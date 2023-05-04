using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody targetRb;
    public ParticleSystem explosionParticle;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -6;
    
    public int pointValue;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -7)
        {
            Destroy(gameObject);
            if(!gameObject.CompareTag("Bad") && gameManager.isGameActive){

                gameManager.UpdateScore(-10);
                gameManager.UpdateHealth(-1);
            }
        }
        
    }
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
    private void OnMouseOver()
    {
        if(gameManager.isGameActive && !gameManager.isPaused && Input.GetMouseButton(0))
        {
            GameObject.Find("Main Camera").GetComponent<CursorTrail>().enabled = true;
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
            if(gameObject.CompareTag("Bad"))
            {
                gameManager.GameOver();
            }
        }
    }
}
