using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{
    Vector3 spawnPosition;
    Quaternion spawnRotation;
    float y;
    [SerializeField]
    int unitsOut = 10;

    float spawnTime;
    float timer;
    [SerializeField]
    float lowTime = 5.0f;
    [SerializeField]
    float highTime = 15.0f;
    bool readyToSpawn = false;

    [SerializeField]
    GameObject[] obstacles;
    int obstacleNumber;

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpawn();
        UpdateTimer();
    }

    void UpdateSpawn ()
    {
        UpdateY();
        spawnPosition = new Vector3(Random.Range(-4.0f, 4.0f), y, 1);
        spawnRotation = GetComponent<Transform>().rotation;
    }

    void UpdateY ()
    {
        y = GetComponent<Transform>().position.y + unitsOut;
    }

    void UpdateTimer()
    {
        if (timer == 0)
        {
            GetSpawnTime();
        }

        if (readyToSpawn == true)
        {
            timer += Time.deltaTime;
            if (timer > spawnTime)
            {
                ChooseObstacle();
                Instantiate(obstacles[obstacleNumber], spawnPosition, spawnRotation);
                timer = 0;
                readyToSpawn = false;
            }
        }
    }

    void GetSpawnTime()
    {
        spawnTime = Random.Range(lowTime, highTime);
        readyToSpawn = true;
    }

    void ChooseObstacle()
    {
        obstacleNumber = Random.Range(0, obstacles.Length);
    }
}
