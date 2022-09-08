using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Gun")]
public class GunData : ItemData
{
    public float fireRate = 0.5f;
    public float firePower;
    public float reloadTime = 1f;
    public int maxAmmo = -1;
    public int ammoPerClip = -1;
    public GunType gunType;
}

public enum GunType
{
    Automatic,
    SemiAuto,
    Burst
}
