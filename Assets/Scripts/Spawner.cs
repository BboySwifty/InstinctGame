using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnPrefab;

    private bool _isEnabled = false;

    public GameObject SpawnZombie()
    {
        GameObject zombie = Instantiate(spawnPrefab, transform);
        zombie.GetComponent<AIDestinationSetter>().target = Globals.Instance.player.transform;
        return zombie;
    }

    public void SetEnabled(bool enabled)
    {
        _isEnabled = enabled;
    }

    public bool IsEnabled()
    {
        return _isEnabled;
    }
}
