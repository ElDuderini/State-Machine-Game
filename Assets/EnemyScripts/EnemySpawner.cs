﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("prefab for the enemy that will be spawned")]
    public GameObject enemyPrefab;
    [Tooltip("time it takes between spawning enemies")]
    public float timeToSpawn = 1.65f;
    [Tooltip("maximum number of enemies that can be alive at once")]
    public int maxEnemies = 7;
    [Tooltip("total enemies the spawner will spawn, 0 = infinite")]
    public int totalEnemies = 0;
    [Tooltip("whether to spawn an enemy immediately when the spawner is loaded")]
    public bool spawnEnemyAtStart = true;

    private int enemiesSpawned;
    private float time;

    private void Start()
    {
        if(spawnEnemyAtStart)
        {
            Instantiate(enemyPrefab, transform.position, enemyPrefab.transform.rotation, transform);
            enemiesSpawned++;
        }
    }

    void Update()
    {
        //spawn an enemy if the max number of enemies are not alive and the total number of enemies have not been met (if not infinite)
        if(transform.childCount < maxEnemies && enemiesSpawned < totalEnemies || transform.childCount < maxEnemies && totalEnemies == 0)
        {
            time += Time.deltaTime;
            if(time >= timeToSpawn)
            {
                Instantiate(enemyPrefab, transform.position, enemyPrefab.transform.rotation, transform);
                time = 0;
                enemiesSpawned++;
            }
        }
    }
}