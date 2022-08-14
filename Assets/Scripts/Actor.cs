using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public float Health;
    public float moveSpeed = 1f;
    public float turnSpeed = 3f;
    public float KnockbackMultiplier = 1f;
    public float KnockbackTime = 0.5f;
    public Animator playerDamaged;
    public AudioSource deathSound;
    protected Rigidbody2D rb;
    protected bool knockedBack = false;
    protected float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = Health;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    protected void Aim(Vector3 target)
    {
        Vector2 dir = transform.position - target;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));
    }

    public virtual void Move(Vector2 movement)
    {
        if (!knockedBack && movement != Vector2.zero)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        }
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;

        if (IsDying())
            Die();
    }

    public void Knockback(Vector3 impulse)
    {
        knockedBack = true;
        playerDamaged.SetBool("playerKnocked", true);
        rb.AddForce(impulse * KnockbackMultiplier, ForceMode2D.Impulse);
        StartCoroutine(KnockbackCO());
    }

    private IEnumerator KnockbackCO()
    {
        yield return new WaitForSeconds(KnockbackTime);
        rb.velocity = Vector2.zero;
        knockedBack = false;
        playerDamaged.SetBool("playerKnocked", false);
    }

    public bool IsDying()
    {
        return currentHealth <= 0;
    }

    public void Die()
    {
        rb.isKinematic = true;
        if (deathSound != null && !deathSound.isPlaying && deathSound.enabled)
        {
            deathSound.Play();
        }
        Destroy(gameObject);
    }
}
