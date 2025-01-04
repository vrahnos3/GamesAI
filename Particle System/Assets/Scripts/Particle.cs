using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public Vector3 velocity;
    public Vector3 acceleration;

    private float lifeTime = 5.0f; // Particle lifetime
    private float age = 0.0f;
    public Vector3 lastPos;



    // Start is called before the first frame update
    void Start()
    {
        this.lastPos = transform.position;
    }

    public void InitializeParticle(Vector3 initialVelocity, Vector3 initialAcceleration)
    {
        this.velocity = initialVelocity;
        this.acceleration = initialAcceleration;
    }

    void Update()
    {
        float time = Time.deltaTime;

        velocity += acceleration * time;
        transform.position = lastPos +  velocity * time;
        
        age += time;
        if (age >= lifeTime)
        {
            Destroy(gameObject);
        }
        
    }
}
