using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using static UnityEngine.ParticleSystem;
using UnityEditor.Tilemaps;

public class SeekPursueManager : MonoBehaviour
{
    public GameObject targetObj;
    public GameObject characterObjSeek;
    public GameObject characterObjPursue;

    private Vector2 distance;
    private Vector2 normalizedDistance;
    private Vector2 acceleration1;
    private Vector2 acceleration2;
    public Vector2 maxAcceleration = new Vector2(0.4f, 0.4f);

    private float timer = 0f;
    private float duration = 13f;

    public float maxSpeed = 2;
    public float maxTime = 2;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer <= duration)
        {
            updateCharacter();
        }
        else
        {
            QuitGame();
        }
    }

    Vector2 seek(Character character1, Vector3 targetPosition)
    {
        distance = targetPosition - character1.transform.position;
        normalizedDistance = distance.normalized;
        acceleration1 = normalizedDistance * maxAcceleration;

        return acceleration1;
    }

    Vector2 pursue(Character character2, Target target, float maxTime)
    {   
        
        float t = (target.transform.position - character2.transform.position).magnitude / maxSpeed;
        if (t > maxTime)
        {
            t = maxTime;
        }
        Vector3 prediction = target.position + target.velocity * t;
        acceleration2 = seek(character2, prediction);

        return acceleration2;
    }
    
    void updateCharacter()
    {
        Character characterSeek = characterObjSeek.GetComponent<Character>();
        Character characterPursue = characterObjPursue.GetComponent<Character>();
        Target target = targetObj.GetComponent<Target>();

        Vector3 targetPosition = target.transform.position;

        float time = Time.deltaTime;

        //Update Seek Character
        Vector2 characterSeekAcceleration = seek(characterSeek, targetPosition);
        characterSeek.acceleration = characterSeekAcceleration;
        characterSeek.velocity += (characterSeek.acceleration * time);
        characterSeek.position += (characterSeek.velocity * time);

        characterSeek.transform.position = characterSeek.position;
        RotateCharacter(characterSeek);

        //Update Pursue Character
        maxTime = 5f;
        Vector2 characterPursueAcceleration = pursue(characterPursue, target, maxTime);
        characterPursue.acceleration = characterPursueAcceleration;
        characterPursue.velocity += (characterPursue.acceleration * time);
        characterPursue.position += (characterPursue.velocity * time);

        characterPursue.transform.position = characterPursue.position;
        RotateCharacter(characterPursue);
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
