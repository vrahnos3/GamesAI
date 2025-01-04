using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Vector2 position;
    public Vector2 velocity;
    void Start()
    {
        this.position = transform.position;
        // Comment the above line for Arrive Scene because the target is not moving 
        this.velocity = new Vector2(1f, 0); //for linear movement
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.deltaTime;


        this.position += this.velocity * time; //for linear movement

        //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // for mouse movement
        //this.position = new Vector2(mousePosition.x, mousePosition.y); // for mouse movement

        transform.position = this.position;
    }

   
}
