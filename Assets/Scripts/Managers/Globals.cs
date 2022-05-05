using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    public static Globals Instance { get; private set; }

    public Transform pickupParent;
    public Player player;

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
        player = FindObjectOfType<Player>();
    }
}
