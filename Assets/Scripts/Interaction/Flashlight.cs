using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Flashlight : Item, IUsable
{
    private Light2D _light2D;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        _light2D = GetComponent<Light2D>();
    }

    public void Use()
    {
    }

    public void UseDown()
    {
        Switch();
    }

    private void Switch()
    {
        _light2D.enabled = !_light2D.enabled;
    }
}
