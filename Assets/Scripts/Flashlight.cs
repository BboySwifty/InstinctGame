using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Flashlight : Item
{
    private Light2D _Light2D;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        _Light2D = GetComponent<Light2D>();
    }

    public override void Use()
    {
        _Light2D.enabled = !_Light2D.enabled;
    }
}
