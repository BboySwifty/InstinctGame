using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Actor
{
    public float damage = 10;

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Player player = collider.gameObject.GetComponent<Player>();
            player.Knockback(player.transform.position - transform.position);
            player.Damage(damage);

        }
    }
}
