using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Flashlight : Item
{
    private Light2D _Light2D;

    // Start is called before the first frame update
    public void Start()
    {
        _Light2D = GetComponent<Light2D>();
    }

    public override void Use()
    {
        Switch();
    }

    private void Switch()
    {
        _Light2D.enabled = !_Light2D.enabled;
    }
}
