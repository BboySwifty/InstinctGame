﻿using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    public float SpawnRate = 1f;
    public float WaveRate = 20f;
    public float cooldownTimer = 0f;
    public GameObject ZombiesContainer;
    public GameObject Zombie;
    public List<Transform> SpawnPoints;
    public bool endless = false;
    public bool debug = false;
    public bool waveOnCooldown = true;
    public int waveNumber = 0;

    private Player player;
    private float spawnTimer = 0f;
    private int zombiesToSpawn = 5;
    private int numberOfZombiesAlive = 0;

    #region Singleton
    private void CreateInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Awake()
    {
        CreateInstance();
    }

    private void Start()
    {
        player = Globals.Instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            if (waveOnCooldown){
                cooldownTimer += Time.deltaTime;
                if (cooldownTimer >= WaveRate)
                {
                    NextWave();
                }
            }
            else if (zombiesToSpawn > 0 && SpawnPoints.Count > 0)
            {
                spawnTimer += Time.deltaTime;
                if (spawnTimer >= SpawnRate && numberOfZombiesAlive < 20)
                {
                    int spawnPointIndex = GetRandomIndex();
                    SpawnZombie(SpawnPoints[spawnPointIndex]);
                    spawnTimer = 0f;
                }
            }
            else if (numberOfZombiesAlive <= 0)
            {
                waveOnCooldown = true;
                numberOfZombiesAlive = 0;
            }
        }
    }

    private void SpawnZombie(Transform spawnPoint)
    {
        GameObject zombie = Instantiate(Zombie, spawnPoint);
        zombie.GetComponent<AIDestinationSetter>().target = player.transform;
        zombie.transform.SetParent(ZombiesContainer.transform);
        zombiesToSpawn--;
        numberOfZombiesAlive++;
        
        if (debug)
        {
            Destroy(zombie, 1f);
            numberOfZombiesAlive--;
        }
    }

    private int GetRandomIndex()
    {
        return UnityEngine.Random.Range(0, SpawnPoints.Count);
    }

    private void NextWave()
    {
        cooldownTimer = 0f;
        waveOnCooldown = false;
        waveNumber++;
        zombiesToSpawn = waveNumber * 2;
        if (endless)
            zombiesToSpawn = 9999;
        spawnTimer = -2f;
    }

    public void ChangeSpawnPoints(List<Transform> sp)
    {
        SpawnPoints = sp;
    }

    public void ZombieKilled()
    {
        numberOfZombiesAlive--;
    }
}