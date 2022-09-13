using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{
    public override void Use()
    {
        Destroy(gameObject);
    }
}
