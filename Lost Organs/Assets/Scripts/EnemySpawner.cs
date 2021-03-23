using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;

    GameObject enemy;
    Vector3[] spawnPoints;
    float countDown = 8;

    bool start = false;

    void Start()
    {
        spawnPoints = new Vector3[10];
        spawnPoints[0] = new Vector3(-2.87f, 0.9f, 117);
        spawnPoints[1] = new Vector3(21.4f, 0.9f, 117);
        spawnPoints[2] = new Vector3(50, 0.9f, 117);
        spawnPoints[3] = new Vector3(74, 0.9f, 117);
        spawnPoints[4] = new Vector3(63, 1.24f, 72.5f);
        spawnPoints[5] = new Vector3(46.5f, 1.24f, 64.3f);
        spawnPoints[6] = new Vector3(22.5f, 1.24f, 61);
        spawnPoints[7] = new Vector3(0, 1.24f, 61);
    }

    void Update()
    {
        if(!start && Input.GetKeyDown(KeyCode.Space))
        {
            start = true;
            GameManager.gm.StartUI();
        }
        if (!GameManager.gm.gameOver && start)
        {
            countDown -= Time.deltaTime;
            if (countDown < 0)
            {
                SpawnEnemy();
                countDown = CalculateCountdown();
            }
        }
    }

    float CalculateCountdown()
    {
        float cd = 5 * Mathf.Pow(1 / 1.05f, Time.time) + 3;
        return cd;
    }

    void SpawnEnemy()
    {
        Vector3 spawnPoint = spawnPoints[Random.Range(0, 8)];

        if(Random.Range(0, 2) == 0)
        {
            enemy = Instantiate(enemy1, spawnPoint, Quaternion.identity, transform);
        }
        else
        {
            enemy = Instantiate(enemy2, spawnPoint, Quaternion.identity, transform);
        }
        enemy.SetActive(true);
    }
}
