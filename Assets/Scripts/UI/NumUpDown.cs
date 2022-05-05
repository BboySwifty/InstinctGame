using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumUpDown : MonoBehaviour
{
    public TextMeshProUGUI display;

    public int value = 0;

    // Start is called before the first frame update
    void Start()
    {
        display.text = value.ToString();
    }

    public void Increment()
    {
        value = value == 9 ? 0 : value + 1;
        display.text = value.ToString();
    }

    public void Decrement()
    {
        value = value == 0 ? 9 : value - 1;
        display.text = value.ToString();
    }
}
