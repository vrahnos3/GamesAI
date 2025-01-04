using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;


public class BirdManager : MonoBehaviour
{
    
    public GameObject birdPrefab;
    private List<Bird> birds;

    [Header("")]
    public int numberOfBirds = 10;
    public int boundHeight = 10;
    public int boundWidth = 10;
    void Start()
    {
        CreateBirds();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < birds.Count; i++)
        {
            birds[i].UpdateBird(birds);
        }
    }

    public void CreateBirds()
    {
        GameObject liveSimulation = new GameObject("Flocking Birds");

        birds = new List<Bird>();

        for (int i = 0; i < numberOfBirds; i++)
        {
            float x = Random.Range(-boundWidth / 2f, boundWidth / 2f);
            float z = Random.Range(-boundHeight / 2f, boundHeight / 2f);
            Vector3 position = transform.position + new Vector3(x, 0, z);

            Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

            GameObject birdObject = Instantiate(birdPrefab, position, rotation);
            birdObject.transform.SetParent(liveSimulation.transform);

            Bird b = birdObject.GetComponent<Bird>();
            b.Initialize(position, rotation);

            birds.Add(birdObject.GetComponent<Bird>());
        }
    }
}
