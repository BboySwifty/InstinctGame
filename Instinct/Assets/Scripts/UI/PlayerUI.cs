using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public Transform player;
    public Vector3 adjustmentVector;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + adjustmentVector;
    }
}
