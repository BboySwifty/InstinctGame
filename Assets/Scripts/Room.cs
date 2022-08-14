﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Room> AdjacentRooms;
    public List<Spawner> SpawnPoints;
    public bool Active = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RefreshSpawns();
        }
    }

    public void RefreshSpawns()
    {
        List<Spawner> allSpawns = new List<Spawner>();
        allSpawns.AddRange(SpawnPoints);
        foreach (Room room in AdjacentRooms)
        {
            if (room.Active)
                allSpawns.AddRange(room.SpawnPoints);
        }
        WaveManager.Instance.ChangeSpawnPoints(allSpawns);
    }
}
