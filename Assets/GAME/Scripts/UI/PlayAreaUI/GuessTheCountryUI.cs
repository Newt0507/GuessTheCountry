using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GuessTheCountryUI : MonoBehaviour
{
    private CanvasGroup _canvas;
    private RectTransform _rect;
    
    private void Awake()
    {
        _canvas = GetComponent<CanvasGroup>();
        _rect = GetComponent<RectTransform>();

        Init();
    }

    private void Init()
    {
        _rect.anchoredPosition = new Vector2(0, -200);
        _canvas.alpha = 0;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Sequence sequence = DOTween.Sequence();

        float duration = 1.5f;
        sequence.Append(_rect.DOAnchorPosY(0, duration))
            .Join(_canvas.DOFade(1, duration));

        sequence.Append(_canvas.DOFade(0, duration / 3));
    }
}
