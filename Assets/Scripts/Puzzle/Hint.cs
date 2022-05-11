using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public Image ExamineImage;

    public void SetSprite(Sprite sprite)
    {
        if(SpriteRenderer != null)
            SpriteRenderer.sprite = sprite;
    }

    public void SetExamineImage(Sprite sprite)
    {
        if (ExamineImage != null)
            ExamineImage.sprite = sprite;
    }
}
