using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Vector2 position;
    public Vector2 velocity;
    public Vector2 acceleration;

    void Start()
    {
        this.position = transform.position;
        Debug.Log(this.position);
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.deltaTime;

        this.velocity += this.acceleration * time;
        this.position += velocity * time;
        transform.position = this.position;


    }
}
