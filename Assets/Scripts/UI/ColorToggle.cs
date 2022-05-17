using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorToggle : MonoBehaviour
{
    public Colors[] colors;
    public Image colorImage;

    public enum Colors
    {
        Red,
        Yellow,
        Green,
        Blue,
        Purple
    }

    private Dictionary<Colors, string[]> colorsHex = new Dictionary<Colors, string[]>()
    {
        { Colors.Red, new[] { "R", "#D81D1D" } },
        { Colors.Yellow, new[] { "Y", "#C2B31D" } },
        { Colors.Green, new[] { "G", "#32AB08" } },
        { Colors.Blue, new[] { "B", "#2A70FF" } },
        { Colors.Purple, new[] { "P", "#DA00A5" } },

    };
    private string value;
    private int index = 0;

    void Start()
    {
        SetImageColor(colors[index]);
        SetCodeValue(colors[index]);
    }

    public void NextColor()
    {
        index++;
        if (index == colors.Length)
            index = 0;
        SetImageColor(colors[index]);
        SetCodeValue(colors[index]);
    }

    private void SetCodeValue(Colors color)
    {
        value = colorsHex[color][0];
    }

    private void SetImageColor(Colors color)
    {
        Color parsedColor;
        if(ColorUtility.TryParseHtmlString(colorsHex[color][1], out parsedColor))
        {
            colorImage.color = parsedColor;
        }
    }

    public string GetCodeValue()
    {
        return value;
    }
}
