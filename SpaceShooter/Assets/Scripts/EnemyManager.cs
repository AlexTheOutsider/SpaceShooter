using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    private Transform[] spawnPoints;
    private bool waveCleared = true;

    private int waveIndex;
    private int enemyNumber;
    private int enemyLeft;

    private void Start()
    {
        spawnPoints = GameObject.Find("Spawn Points").transform.GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        if (!waveCleared) return;

        for (int i = 0; i < enemyNumber; i++)
        {
            Instantiate(Resources.Load("Prefabs/Enemy"), GetRandomSpawnPosition(), spawnPoints[0].rotation);
        }
        waveIndex++;
        waveCleared = false;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)].position;
    }
}