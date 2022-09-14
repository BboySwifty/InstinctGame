using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player : Actor
{
    public GameObject legs;

    public float score = 0;
    public int state = 0; // States : 0 = Nothing, 1 = Gun, 2 = Item
    public bool isWalking = false;

    public event EventHandler<HealthChangedEventArgs> HealthChanged;

    private Animator legsAnimator;

    #region Singleton
    public static Player Instance { get; private set; }

    public void CreateInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public void Awake()
    {
        CreateInstance();
    }

    public override void Start()
    {
        base.Start();
        legsAnimator = legs.GetComponent<Animator>();
    }

    private void Update()
    {
        legsAnimator.SetBool("Walking", isWalking);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Aim(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public override void Damage(float damage)
    {
        base.Damage(damage);
        HealthChanged?.Invoke(this, new HealthChangedEventArgs(currentHealth));
    }

    public override void Move(Vector2 movement)
    {
        if (!knockedBack && movement != Vector2.zero)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

            /* Rotate the legs */
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            legs.transform.rotation = Quaternion.Slerp(legs.transform.rotation, Quaternion.Euler(0, 0, angle + 180), turnSpeed * Time.deltaTime);
        }
    }

    public override void Die()
    {
        gameObject.SetActive(false);
    }

    public void AddScore(float score)
    {
        this.score += score;
    }

    public void SetState(int state)
    {
        this.state = state;
    }

    public void SetWalkAnimation(bool value)
    {
        legsAnimator.SetBool("Walking", value);
    }
}

public class HealthChangedEventArgs
{
    public float CurrentHealth { get; }

    public HealthChangedEventArgs(float currentHealth)
    {
        CurrentHealth = currentHealth;
    }
}