using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialChest : MonoBehaviour
{

    public event EventHandler CollisionExit;

    private void OnTriggerExit2D(Collider2D collision)
    {
        CollisionExit?.Invoke(this, EventArgs.Empty);
    }
}
