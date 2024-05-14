using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class OptionBackgroundUI : MonoBehaviour
{
    [SerializeField] private OptionMenuUI _optionMenuUI;
    
    private CanvasGroup _canvas;

    private void Awake()
    {
        _canvas = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        _canvas.alpha = 0;
    }
}
