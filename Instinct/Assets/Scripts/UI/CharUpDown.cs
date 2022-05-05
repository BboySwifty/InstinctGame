using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharUpDown : MonoBehaviour
{
    public TextMeshProUGUI display;
    public char[] value;

    private int index;

    void Start()
    {
        display.text = value[0].ToString();
    }

    public void Increment()
    {
        index = index == value.Length - 1 ? 0 : index + 1;
        display.text = value[index].ToString();
    }

    public void Decrement()
    {
        index = index == 0 ? value.Length - 1 : index - 1;
        display.text = value[index].ToString();
    }

    public char GetValue()
    {
        return value[index];
    }
}
