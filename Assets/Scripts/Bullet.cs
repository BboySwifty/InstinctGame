
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject hitEffect;
    public float lifetime = 1f;
    public float damage = 1f;
    public float scorePerHit = 10f;

    private Player playerStats;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collider = collision.gameObject;
        if (!collider.CompareTag("Player") && !collision.isTrigger)
        {
            Actor character = collider.GetComponent<Actor>();
            if (collider.CompareTag("Enemy") && !character.IsDying())
            {
                character.Damage(damage);
                if (character.IsDying())
                {
                    character.Die();
                    playerStats.AddScore(50f);
                }
                playerStats.AddScore(scorePerHit);
            }
            Destroy(gameObject);
        }
    }

    public void SetPlayerStats(Player stats)
    {
        playerStats = stats;
    }
}
