using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Material glowMaterial;

    void Start()
    {
        GetComponent<Image>().material = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().material = glowMaterial;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().material = null;
    }
}
