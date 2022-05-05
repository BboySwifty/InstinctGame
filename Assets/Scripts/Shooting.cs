using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    
    public Gun activeGun;

    //GunInventory inventory;

    void Start()
    {
        //inventory = GunInventory.instance;
        //inventory.ActiveItemChanged += SetActiveGun;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            if (activeGun.CanShoot())
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    activeGun.TriggerDown();
                }
                else if (Input.GetButton("Fire1"))
                {
                    activeGun.Trigger();
                }
            }
            if (Input.GetButtonDown("Reload") && !activeGun.IsReloading())
            {
                StartCoroutine(activeGun.Reload());
            }
        }
    }

    void SetActiveGun(object sender, EventArgs args)
    {
        //activeGun = args.Gun;
    }
}
