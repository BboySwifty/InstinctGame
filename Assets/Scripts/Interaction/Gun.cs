
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Item, IUsable
{
    public Transform firePoint;
    public Animator animator;
    public LineRenderer line;
    public AudioSource shotSound;
    public AudioSource reloadSound;

    public event EventHandler<AmmoChangedEventArgs> AmmoChanged;

    private GunData _gunData;
    private float cooldownTime = 0f;
    private int currentAmmoInClip;
    private int currentExtraAmmo;
    private bool isReloading;
    private const float BULLET_LINE_LIFETIME = 0.02f; // Recommended 0.02f

    public int CurrentClipAmmo => currentAmmoInClip;
    public int CurrentExtraAmmo => currentExtraAmmo;

    private void Awake()
    {
        _gunData = itemData as GunData;
    }

    public override void Start()
    {
        base.Start();
        currentAmmoInClip = _gunData.ammoPerClip;
        currentExtraAmmo = _gunData.maxAmmo == -1 ? _gunData.maxAmmo : _gunData.maxAmmo - _gunData.ammoPerClip;
    }

    void OnEnable()
    {
        isReloading = false;
        //animator.SetBool("Reloading", false);
    }

    private void OnDisable()
    {
        AmmoChanged = null;
    }

    void Update()
    {
        if (!CanShoot())
        {
            cooldownTime += Time.deltaTime;
        }
    }

    public void Use()
    {
        if (!CanShoot())
            return;

        switch (_gunData.gunType)
        {
            case GunType.Automatic:
                ShootOrReload();
                break;
            case GunType.SemiAuto:
                break;
            case GunType.Burst:
                break;
        }
    }

    public void UseDown()
    {
        if (!CanShoot())
            return;

        switch (_gunData.gunType)
        {
            case GunType.Automatic:
                ShootOrReload();
                break;
            case GunType.SemiAuto:
                ShootOrReload();
                break;
            case GunType.Burst:
                break;
        }
    }

    public bool CanShoot()
    {
        return cooldownTime >= _gunData.fireRate;
    }

    private void ShootOrReload()
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
                zombie.Damage(_gunData.firePower);
            }
            StartCoroutine(ShowBulletLine(firePoint.position, hit.point, BULLET_LINE_LIFETIME));
        }
        else
        {
            StartCoroutine(ShowBulletLine(firePoint.position, firePoint.position + firePoint.up * 10, BULLET_LINE_LIFETIME));
        }

        cooldownTime = 0f;

        if (_gunData.ammoPerClip != -1)
            currentAmmoInClip--;

        InvokeAmmoChanged();
    }

    public IEnumerator Reload()
    {
        if (currentAmmoInClip < _gunData.ammoPerClip && _gunData.ammoPerClip != -1 && currentExtraAmmo != 0)
        {
            reloadSound.Play();
            isReloading = true;
            //animator.SetBool("Reloading", true);
            yield return new WaitForSeconds(_gunData.reloadTime - .25f);

            int ammoToReload = _gunData.ammoPerClip - currentAmmoInClip;

            if (_gunData.maxAmmo != -1)
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

        InvokeAmmoChanged();
    }

    private IEnumerator ShowBulletLine(Vector2 from, Vector2 to, float time)
    {
        line.SetPosition(0, from);
        line.SetPosition(1, to);
        line.enabled = true;
        yield return new WaitForSeconds(time);
        line.enabled = false;
    }

    private void InvokeAmmoChanged()
    {
        AmmoChanged?.Invoke(this, new AmmoChangedEventArgs(currentAmmoInClip, currentExtraAmmo));
    }
}

public class AmmoChangedEventArgs : EventArgs
{
    public int CurrentAmmo { get; }
    public int ExtraAmmo { get; }

    public AmmoChangedEventArgs(int currentAmmo, int extraAmmo)
    {
        CurrentAmmo = currentAmmo;
        ExtraAmmo = extraAmmo;
    }
}