using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExamineableHint : RandomHint
{
    public Image examineImage;

    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        examineImage.sprite = spriteRenderer.sprite;
    }
}
