using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Usable
{
    public override void Use()
    {
        Destroy(gameObject);
    }
}
