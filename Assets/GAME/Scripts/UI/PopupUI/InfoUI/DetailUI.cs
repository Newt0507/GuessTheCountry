using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailUI : MonoBehaviour
{
    [SerializeField] private Image _flag;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _infoText;

    private RectTransform _rect;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        
        Init();
    }

    private void Init()
    {
        _rect.anchoredPosition = new Vector2(0, -1500);
    }

    public void Show()
    {
        _rect.DOAnchorPosY(-180, 1).SetDelay(1);
    }
    
    
    public void SetFlag(Sprite flag)
    {
        _flag.sprite = flag;
    }
    
    public void SetName(string name)
    {
        _name.text = name;
    }
    
    public void SetInfoText(string infoText)
    {
        _infoText.text = infoText;
    }
}