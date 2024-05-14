using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartBackgroundUI : MonoBehaviour
{
    private Image _image;
    
    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.color = Color.black;
    }

    private void OnEnable()
    {
        _image.DOFade(0, 2);
    }
}
