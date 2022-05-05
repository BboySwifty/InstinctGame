using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIPopup : MonoBehaviour
{
    private RectTransform rectTransform;
    private bool _initialized = false;

    private void OnEnable()
    {
        if (_initialized)
        {
            rectTransform.DOScale(1, 1f);
        }
    }

    private void OnDisable()
    {
        rectTransform.DOScale(0, 1f);
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        gameObject.SetActive(false);
        _initialized = true;
    }
}
