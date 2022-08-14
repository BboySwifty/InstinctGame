using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTimelineTrigger : MonoBehaviour
{
    public event EventHandler TriggerEntered;
    public int id;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            TriggerEntered?.Invoke(this, EventArgs.Empty);
    }
}
