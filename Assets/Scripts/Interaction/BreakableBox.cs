using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBox : InteractableObject
{
    public Sprite brokenImage;
    public GameObject itemPrefab;
    public ItemData itemDrop;
    public SpriteRenderer spriteRenderer;
    public bool disableCollider = false;

    private ParticleSystem _particles;

    private void Start()
    {
        _particles = GetComponent<ParticleSystem>();
    }

    public override void Use()
    {
        enabled = false;

        if(_particles != null)
            _particles.Play();

        spriteRenderer.sprite = brokenImage;

        foreach (Collider2D c in GetComponents<Collider2D>())
        {
            if(c.isTrigger || disableCollider) // Only disable trigger if disableCollider is false
                c.enabled = false;
        }

        itemPrefab.GetComponent<Item>().itemData = itemDrop;
        Instantiate(itemPrefab, transform.position, Quaternion.identity, Globals.Instance.pickupParent);
    }
}
