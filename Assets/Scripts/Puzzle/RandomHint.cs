using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHint : MonoBehaviour
{

    
    [HideInInspector]
    public string value;

    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    public virtual void Awake()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(folderPath);
        int index = Random.Range(0, sprites.Length);
        spriteRenderer.sprite = sprites[index];
        value = sprites[index].name.Substring(sprites[index].name.IndexOf('_') + 1);
    }
}
