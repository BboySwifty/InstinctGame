using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodNumber : MonoBehaviour
{
    [HideInInspector]
    public int value = 0;

    void Awake()
    {
        value = Random.Range(0, 10);
        Sprite sp = Resources.Load<Sprite>("Sprites/Blood Numbers/BloodNumbers_" + value.ToString());
        GetComponent<SpriteRenderer>().sprite = sp;
    }
}
