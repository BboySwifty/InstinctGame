using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Item
{
    public float fireRate = 0.5f;
    public float firePower;
    public float bulletVelocity = 20f;
    public float bulletLifetime = 1f;
    public float reloadTime = 1f;
    public int maxAmmo = -1;
    public int ammoPerClip = -1;
    public GunType gunType;
    public Transform firePoint;
    public AudioSource shotSound;
    public GameObject bulletPrefab;
    public Animator animator;

    private float cooldownTime = 0f;
    private int currentAmmoInClip;
    private int currentExtraAmmo;
    private bool isReloading;

    private void Awake()
    {
        inventoryIndex = 0;
        currentAmmoInClip = ammoPerClip;
        currentExtraAmmo = maxAmmo == -1 ? maxAmmo : maxAmmo - ammoPerClip;
    }

    void OnEnable()
    {
        isReloading = false;
        //animator.SetBool("Reloading", false);
    }

    void Update()
    {
        if (!CanShoot())
        {
            cooldownTime += Time.deltaTime;
        }
    }

    public override void Use()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (currentAmmoInClip != 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Bullet>().SetPlayerStats(player);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletVelocity, ForceMode2D.Impulse);
            cooldownTime = 0f;
            if (ammoPerClip != -1)
                currentAmmoInClip--;
        }
        else
        {
            // Play empty gun sound
            if (!IsReloading())
                StartCoroutine(Reload());
        }
    }

    public IEnumerator Reload()
    {
        if (currentAmmoInClip < ammoPerClip && ammoPerClip != -1 && currentExtraAmmo != 0)
        {
            isReloading = true;
            //animator.SetBool("Reloading", true);
            yield return new WaitForSeconds(reloadTime - .25f);

            int ammoToReload = ammoPerClip - currentAmmoInClip;

            if (maxAmmo != -1)
            {
                if (ammoToReload > currentExtraAmmo)
                    ammoToReload = currentExtraAmmo;
                currentExtraAmmo -= ammoToReload;
            }

            currentAmmoInClip += ammoToReload;
            isReloading = false;
            //animator.SetBool("Reloading", false);
            yield return new WaitForSeconds(.25f);
        }
        else
        {
            // Play can't reload sound
        }
    }

    public bool CanShoot()
    {
        return cooldownTime >= fireRate;
    }

    public bool IsReloading()
    {
        return isReloading;
    }

    public int CurrentClipAmmo()
    {
        return currentAmmoInClip;
    }

    public int CurrentExtraAmmo()
    {
        return currentExtraAmmo;
    }

    public void Trigger()
    {
        if (CanShoot())
        {
            switch (gunType)
            {
                case GunType.Automatic:
                    Shoot();
                    break;
                case GunType.SemiAuto:
                    break;
                case GunType.Burst:
                    Shoot();
                    break;
            }
        }
    }

    public void TriggerDown()
    {
        if (CanShoot())
        {
            switch (gunType)
            {
                case GunType.Automatic:
                    Shoot();
                    break;
                case GunType.SemiAuto:
                    Shoot();
                    break;
                case GunType.Burst:

                    break;
            }
        }
    }

    public enum GunType
    {
        Automatic,
        SemiAuto,
        Burst
    }
}
