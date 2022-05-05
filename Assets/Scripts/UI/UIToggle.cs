using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToggle : MonoBehaviour
{
    public Color OnColor;
    public Color OffColor;
    public float FadeDuration;

    private bool toggled;

    private Image _ToggleImage;

    // Start is called before the first frame update
    void Start()
    {
        _ToggleImage = GetComponent<Image>();
        toggled = false;
        _ToggleImage.color = OffColor;
    }

    public void Toggle()
    {
        toggled = !toggled;
        _ToggleImage.DOColor(toggled ? OnColor : OffColor, FadeDuration);
    }

    public bool IsOn()
    {
        return toggled;
    }
}
