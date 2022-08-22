
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Item
{
    public float fireRate = 0.5f;
    public float firePower;
    public float reloadTime = 1f;
    public int maxAmmo = -1;
    public int ammoPerClip = -1;
    public GunType gunType;
    public Transform firePoint;
    public Animator animator;
    public LineRenderer line;
    public AudioSource shotSound;
    public AudioSource reloadSound;

    private float cooldownTime = 0f;
    private int currentAmmoInClip;
    private int currentExtraAmmo;
    private bool isReloading;
    private const float BULLET_LINE_LIFETIME = 0.02f; // Recommended 0.02f

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
        if (isReloading)
            return;

        if (currentAmmoInClip > 0)
            Shoot();
        else
            StartCoroutine(Reload());
    }

    private void Shoot()
    {
        shotSound.Play();
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.up, 10f);
        
        if (hit)
        {
            Zombie zombie = hit.transform.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.Damage(firePower);
            }
            StartCoroutine(ShowBulletLine(firePoint.position, hit.point, BULLET_LINE_LIFETIME));
        }
        else
        {
            StartCoroutine(ShowBulletLine(firePoint.position, firePoint.position + firePoint.up * 10, BULLET_LINE_LIFETIME));
        }

        cooldownTime = 0f;

        if (ammoPerClip != -1)
            currentAmmoInClip--;
    }

    public IEnumerator Reload()
    {
        if (currentAmmoInClip < ammoPerClip && ammoPerClip != -1 && currentExtraAmmo != 0)
        {
            reloadSound.Play();
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
            //animator.SetBool("Reloading", false);
            yield return new WaitForSeconds(.25f);
            isReloading = false;
        }
        else
        {
            // Play can't reload sound
        }
    }

    private IEnumerator ShowBulletLine(Vector2 from, Vector2 to, float time)
    {
        line.SetPosition(0, from);
        line.SetPosition(1, to);
        line.enabled = true;
        yield return new WaitForSeconds(time);
        line.enabled = false;
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
        if (!CanShoot())
            return;

        switch (gunType)
        {
            case GunType.Automatic:
                Use();
                break;
            case GunType.SemiAuto:
                break;
            case GunType.Burst:
                Use();
                break;
        }
    }

    public void TriggerDown()
    {
        if (!CanShoot())
            return;

        switch (gunType)
        {
            case GunType.Automatic:
                Use();
                break;
            case GunType.SemiAuto:
                Use();
                break;
            case GunType.Burst:
                break;
        }
    }
}
