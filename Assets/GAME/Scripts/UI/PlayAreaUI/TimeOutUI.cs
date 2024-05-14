using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TimeOutUI : MonoBehaviour
{
    private CanvasGroup _canvas;
    
    private void Awake()
    {
        _canvas = GetComponent<CanvasGroup>();

        Init();
    }

    private void Init()
    {
        transform.localScale = Vector3.one * 0.5f;
        _canvas.alpha = 0;
        gameObject.SetActive(false);
    }
    
    private void OnEnable()
    {
        transform.DOScale(Vector3.one, 1);
        _canvas.DOFade(1, 1);
    }
}
