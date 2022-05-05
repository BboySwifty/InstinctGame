using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareLockHint : MonoBehaviour
{
    [HideInInspector]
    public string Value;

    void Awake()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/SquareLock Hints");
        int index = Random.Range(0, sprites.Length);
        GetComponent<SpriteRenderer>().sprite = sprites[index];
        Value = sprites[index].name.Replace("SquareLock_", "");
    }
}
