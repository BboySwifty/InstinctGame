using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OldDoor : MonoBehaviour
{

    public TextMeshPro unlockMessage;
    public Room room;
    public int doorIndex;
    public string message;
    public float cost;

    private WaveManager wm;
    private AstarManager am;

    private void Start()
    {
        wm = WaveManager.Instance;
        am = AstarManager.Instance;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Player player = collider.GetComponent<Player>();
            //player.SetNearbyDoor(this);
            unlockMessage.SetText(message + string.Format("\n(Cost : {0})", cost));
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Player player = collider.GetComponent<Player>();
            //player.SetNearbyDoor(null);
            unlockMessage.SetText("");
        }
    }

    public void Open()
    {
        room.Active = true;
        room.RefreshSpawns();
        Destroy(gameObject);
        am.ScanDoorArea(GetComponent<BoxCollider2D>().bounds);
    }
}
