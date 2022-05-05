using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Examine : MonoBehaviour
{

    public enum Direction { Up, Down }
    public Direction direction;

    [HideInInspector]
    public GameObject panel;
    private RectTransform _panelRT;
    private Sequence _openSequence;

    // Start is called before the first frame update
    public virtual void Start()
    {
        panel = transform.Find("Canvas").gameObject;
        _panelRT = panel.GetComponent<RectTransform>();

        StartCoroutine(ClosePuzzle());
    }

    private void OnDestroy()
    {
        DOTween.Kill(_panelRT);
    }

    public void OpenPuzzle()
    {
        if (_panelRT == null || panel == null)
            return;

        panel.SetActive(true);
        float posY = direction == Direction.Up ? 1f : -1f;
        _openSequence.Append(_panelRT.DOAnchorPosY(posY, 0.2f));
        _openSequence.Append(_panelRT.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
    }

    public IEnumerator ClosePuzzle()
    {
        if (_panelRT == null || panel == null)
            yield return null;

        _panelRT.DOAnchorPosY(0f, 0.2f);
        _panelRT.DOScale(new Vector2(0f, 0f), 0.2f);
        yield return new WaitForSeconds(0.2f);
        panel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        OpenPuzzle();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        StartCoroutine(ClosePuzzle());
    }
}
