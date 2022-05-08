using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Image _examineImage;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _examineImage = GetComponent<Image>();
    }

    protected void SetSprite(Sprite sprite)
    {
        if(_spriteRenderer != null)
            _spriteRenderer.sprite = sprite;
    }

    protected void SetExamineImage(Sprite sprite)
    {
        if (_examineImage != null)
            _examineImage.sprite = sprite;
    }
}
