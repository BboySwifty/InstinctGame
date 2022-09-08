using Pathfinding;
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
    public List<Spawner> SpawnPoints;
    public bool endless = false;
    public bool debug = false;
    public bool waveOnCooldown = true;
    public int waveNumber = 0;

    private float spawnTimer = 0f;
    private int zombiesToSpawn = 5;
    private GameObject player;

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
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameIsPaused)
            return;

        if (GameManager.Instance.currentState == GameState.Tutorial)
            return;

        if (waveOnCooldown)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= WaveRate)
            {
                NextWave();
            }
        }
        else if (zombiesToSpawn > 0 && SpawnPoints.Count > 0)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= SpawnRate && ZombiesContainer.transform.childCount < 20)
            {
                int spawnPointIndex = GetRandomIndex();
                SpawnZombie(SpawnPoints[spawnPointIndex]);
                spawnTimer = 0f;
            }
        }
        else if (ZombiesContainer.transform.childCount == 0)
        {
            waveOnCooldown = true;
        }
    }

    private void SpawnZombie(Spawner spawnPoint)
    {
        GameObject zombie = spawnPoint.SpawnZombie();
        zombie.transform.SetParent(ZombiesContainer.transform);
        zombie.GetComponent<AIDestinationSetter>().target = player.transform;
        zombiesToSpawn--;

        if (debug)
        {
            Destroy(zombie, 1f);
        }
    }

    private int GetRandomIndex()
    {
        return UnityEngine.Random.Range(0, SpawnPoints.Count);
    }

    public void NextWave()
    {
        cooldownTimer = 0f;
        waveOnCooldown = false;
        waveNumber++;
        zombiesToSpawn = waveNumber * 2;
        if (endless)
            zombiesToSpawn = 9999;
        spawnTimer = -2f;
    }

    public void ChangeSpawnPoints(List<Spawner> sp)
    {
        SpawnPoints = sp;
    }
}
