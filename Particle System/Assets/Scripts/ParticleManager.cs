using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class ParticleManager : MonoBehaviour
{
    public GameObject particlePrefab;

    [Header("")]
    public Vector3 origin = Vector3.zero;
    public Vector3 cubeSize = new Vector3(8, 8, 8);
    public Vector3 windForce = new Vector3(3f, 0, 0);
    public Vector3 gravity = new Vector3(0, -9.8f, 0);

    [Header("")]
    public float e = 0.7f;
    public float maxInitVelocity = 15f;
    public float minInitVelocity = 0f;
    public int spawnRate = 1;
    private float spawnTimer = 0.0f;

    private List<GameObject> ActiveParticles = new List<GameObject>();
    private float collisionTime;
    

    // Start is called before the first frame update
    void Start()
    {   
        //In case I want to spawn only one particle
        //SpawnParticle();
    }

    // Update is called once per frame
    void Update()
    {
        
        //In case I want to spawn multiple particles
        spawnTimer += Time.deltaTime;
        while (spawnTimer > 1.0f / spawnRate)
        {
            spawnTimer -= 1.0f / spawnRate;
            SpawnParticle();
        }
        //-------------------------------------------
        CheckCollision();
        ActiveParticles.RemoveAll(particle => particle == null);

    }
    void SpawnParticle()
    {
        Vector3 velocity = new Vector3(Random.Range(minInitVelocity, maxInitVelocity), Random.Range(minInitVelocity, maxInitVelocity), Random.Range(minInitVelocity, maxInitVelocity));
        Vector3 acceleration = gravity + windForce;

        GameObject particle = Instantiate(particlePrefab, origin, Quaternion.identity);
        particle.GetComponent<Particle>().InitializeParticle(velocity, acceleration);

        ActiveParticles.Add(particle);

    }

    void CheckCollision()
    {
        foreach (GameObject particleObj in ActiveParticles)
        {
            if (particleObj == null) continue;  // Skip null (destroyed) particles

            Particle particle = particleObj.GetComponent<Particle>();
            Vector3 position = particle.transform.position;
            Vector3 velocity = particle.velocity;
            Vector3 lastPos = particle.lastPos;


            float dTime = Time.deltaTime;

            if (position.x <= -cubeSize.x / 2 || position.x >= cubeSize.x / 2)
            {
                float collisionTime = (position.x <= -cubeSize.x / 2) ? (-cubeSize.x / 2 - lastPos.x) / velocity.x : (cubeSize.x / 2 - lastPos.x) / velocity.x;
                Vector3 collisionPosition = lastPos + velocity * collisionTime;

                velocity.x = -e * velocity.x;
                position = collisionPosition + velocity * (dTime - collisionTime);

                Debug.Log($"Collision occurred at time: {collisionTime}, Position: {collisionPosition}");
            }

            if (position.y <= -cubeSize.y / 2 || position.y >= cubeSize.y / 2)
            {
                float collisionTime = (position.y <= -cubeSize.y / 2) ? (-cubeSize.y / 2 - lastPos.y) / velocity.y : (cubeSize.y / 2 - lastPos.y) / velocity.y;
                Vector3 collisionPosition = lastPos + velocity * collisionTime;

                velocity.y = -e * velocity.y;
                position = collisionPosition + velocity * (dTime - collisionTime);

                Debug.Log($"Collision occurred at time: {collisionTime}, Position: {collisionPosition}");
            }

            if (position.z <= -cubeSize.z / 2 || position.z >= cubeSize.z / 2)
            {
                float collisionTime = (position.z <= -cubeSize.z / 2) ? (-cubeSize.z / 2 - lastPos.z) / velocity.z : (cubeSize.z / 2 - lastPos.z) / velocity.z;
                Vector3 collisionPosition = lastPos + velocity * collisionTime;

                velocity.z = -e * velocity.z;
                position = collisionPosition + velocity * (dTime - collisionTime);

                Debug.Log($"Collision occurred at time: {collisionTime}, Position: {collisionPosition}");
            }

            particle.transform.position = position;
            particle.velocity = velocity;
            particle.lastPos = position;
        }
    }
}
