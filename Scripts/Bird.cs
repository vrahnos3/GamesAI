using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [Header("Boundary Size")]
    private int boundHeight = 10;
    private int boundWidth = 10;

    [Header("Behavior Radious")]
    [SerializeField] public float separationRadious = 1f;
    [SerializeField] public float alignmentRadious = 2f;
    [SerializeField] public float cohesionRadious = 3f;

    [Header("Behavior")]
    public float separationWeight = 0.5f;
    public float alignmentWeight = 0.5f;
    public float cohesionWeight = 0.1f;

    [Header("")]
    public Vector3 velocity;
    public Vector3 position;
    public Vector3 separationVelocity;
    public Vector3 alignmentVelocity;
    public Vector3 cohesionVelocity;

    [SerializeField] public float minSpeed = 0.5f;
    [SerializeField] public float maxSpeed = 10f;
    [SerializeField] public int rotationSpeed = 10;

    public void Initialize(Vector3 position, Quaternion rotation)
    {   
        this.position = position;
        this.velocity = rotation * Vector3.forward * this.maxSpeed;
        
    }

    void UpdateRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(velocity);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void UpdateBird(List<Bird> birds)
    {
        Vector3 separationVelocity = Separation(birds);
        Vector3 alignmentVelocity = Alignment(birds);
        Vector3 cohesionVelocity = Cohesion(birds);

        velocity += separationVelocity;
        velocity += alignmentVelocity;
        velocity += cohesionVelocity;

        ClampBirdVelocity();
        UpdatePosition();
        UpdateRotation();
    }

    public Vector3 Separation(List<Bird> birds)
    {
         int numOfBirdsToAvoid = 0;
         Vector3 separationVelocity = Vector3.zero;
         Vector3 currBirdPosition = transform.position;
         foreach (Bird otherBird in birds)
         {
             if (ReferenceEquals(gameObject, otherBird.gameObject))
             {
                 continue;
             }
             Vector3 otherBirdPosition = otherBird.transform.position;
             float dist = Vector3.Distance(currBirdPosition, otherBirdPosition);
             if (dist < separationRadious)
             {
                 Vector3 otherBirdToCurrBird = currBirdPosition - otherBirdPosition;
                 Vector3 dirToTravel = otherBirdToCurrBird.normalized;
                 Vector3 weightedVelocity = dirToTravel / dist;
                 separationVelocity += weightedVelocity;
                numOfBirdsToAvoid++;
             }
         }
         if (numOfBirdsToAvoid > 0)
         {
             separationVelocity /= (float)numOfBirdsToAvoid;
             separationVelocity *= separationWeight;
         }
         return separationVelocity;
     }

     Vector3 Alignment(List<Bird> birds)
     {
         int numOfBirdsToAlignWith = 0;
         Vector3 alignmentVelocity = Vector3.zero;
         Vector3 currBirdPosition = transform.position;
         foreach (Bird otherBird in birds)
         {
             if (ReferenceEquals(gameObject, otherBird.gameObject))
             {
                 continue;
             }
             Vector3 otherBirdPosition = otherBird.transform.position;
             float dist = Vector3.Distance(currBirdPosition, otherBirdPosition);
             if (dist < alignmentRadious)
             {
                 alignmentVelocity += otherBird.velocity;
                numOfBirdsToAlignWith++;
             }
         }
         if (numOfBirdsToAlignWith > 0)
         {
             alignmentVelocity /= (float)numOfBirdsToAlignWith;
             alignmentVelocity *= alignmentWeight;
         }
         return alignmentVelocity;
     }

     Vector3 Cohesion(List<Bird> birds)
     {
         int numOfBirdsInFlock = 0;
         Vector3 cohesionVelocity = Vector3.zero;
         Vector3 positionToMoveTowards = Vector3.zero;
         Vector3 currBirdPosition = transform.position;
         foreach (Bird otherBird in birds)
         {
             if (ReferenceEquals(gameObject, otherBird.gameObject))
             {
                 continue;
             }
             Vector3 otherBirdPosition = otherBird.transform.position;
             float dist = Vector3.Distance(currBirdPosition, otherBirdPosition);
             if (dist < cohesionRadious)
             {
                 positionToMoveTowards += otherBirdPosition;
                numOfBirdsInFlock++;
             }
         }
         if (numOfBirdsInFlock > 0)
         {
             positionToMoveTowards /= (float)numOfBirdsInFlock;
             Vector3 cohesionDirection = positionToMoveTowards - currBirdPosition;
             cohesionDirection.Normalize();
             cohesionVelocity = cohesionDirection * cohesionWeight;
         }

         return cohesionVelocity;
     }

    void ClampBirdVelocity()
    {
        Vector3 position = transform.position;
        Vector3 direction = velocity.normalized;
        float speed = velocity.magnitude;
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        velocity = direction * speed;
    }

    void UpdatePosition()
    {
        transform.position = transform.position + velocity * Time.deltaTime;

        // outside of the top boundary
        if (transform.position.z > boundHeight)
        {
            transform.position = new Vector3(transform.position.x, 0, -boundHeight);
        }

        // outside of the bottom boundary
        if (transform.position.z < -boundHeight)
        {
            transform.position = new Vector3(transform.position.x, 0, boundHeight);
        }

        // outside of the right boundary
        if (transform.position.x > boundWidth)
        {
            transform.position = new Vector3(-boundWidth, 0, transform.position.z);
        }

        // outside of the left boundary
        if (transform.position.x < -boundWidth)
        {
            transform.position = new Vector3(boundWidth, 0, transform.position.z);
        }
    }
}

