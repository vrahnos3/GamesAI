using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveManager : MonoBehaviour
{
    public GameObject characterObj;
    public GameObject targetObj;

    public float maxSpeed;
    public float targetSpeed;
    public float maxAcceleration;

    float targetRadius = 0.5f;
    float slowRadius = 4.5f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        updateCharacter();

    }

    Vector2 Arrive(Character character, float targetRadius, float slowRadius, float time)
    {
        character = characterObj.GetComponent<Character>();
        Vector3 targetPosition = targetObj.transform.position;

        Vector2 D = targetPosition - character.transform.position;
        float distance = D.magnitude;

        if (distance < targetRadius)
        {
            character.velocity = Vector2.zero;
            //return Vector2.zero;
            QuitGame();

        }
        if (distance > slowRadius)
        {
            targetSpeed = maxSpeed;
        }
        else
        {
            targetSpeed = maxSpeed * distance/slowRadius;
        }

        Vector2 targetVelocity = D.normalized * targetSpeed;
        Vector2 A = (targetVelocity - character.velocity) / time;
        if (A.magnitude > maxAcceleration)
            A = A.normalized * maxAcceleration;

        return A;
    }

    void updateCharacter()
    {
        Character character = characterObj.GetComponent<Character>();

        float time = Time.deltaTime;

        Vector2 characterAcceleration = Arrive(character, targetRadius, slowRadius, time);
        character.acceleration = characterAcceleration;
        character.velocity += (character.acceleration * time);
        character.position += (character.velocity * time);

        character.transform.position = character.position;
        RotateCharacter(character);

    }

    void RotateCharacter(Character character)
    {
        Vector2 direction = character.velocity.normalized;

        if (direction == Vector2.zero) return;

        // Using transform.up as the direction the object should face
        character.transform.up = direction;
    }


    void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
