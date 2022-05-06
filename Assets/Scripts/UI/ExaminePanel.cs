using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExaminePanel : MonoBehaviour
{
    
    private RectTransform _panelRT;

    void Start()
    {
        _panelRT = GetComponent<RectTransform>();

        StartCoroutine(Close());
    }

    private void OnDestroy()
    {
        DOTween.Kill(_panelRT);
    }

    public void Open()
    {
        if (_panelRT == null)
            return;

        enabled = true;
        _panelRT.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
    }

    public IEnumerator Close()
    {
        if (_panelRT == null)
            yield return null;

        _panelRT.DOScale(new Vector2(0f, 0f), 0.2f);
        yield return new WaitForSeconds(0.2f);
        enabled = false;
    }
}
