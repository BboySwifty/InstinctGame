using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorToggle : MonoBehaviour
{
    public Color[] colors;
    public Image colorImage;

    private int index = 0;

    void Start()
    {
        colorImage.color = colors[index];
    }

    public void NextColor()
    {
        index++;
        if (index == colors.Length)
            index = 0;
        colorImage.color = colors[index];
    }

    public Color GetCurrentColor()
    {
        return colors[index];
    }
}
