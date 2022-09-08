using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class ShowSliderValue : MonoBehaviour
{

    private TextMeshProUGUI valueText;

    // Start is called before the first frame update
    void Start()
    {
        valueText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText(float value)
    {
        valueText.text = value.ToString();
    }
}
