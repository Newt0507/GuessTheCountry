using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class LabelUI : MonoBehaviour
{
    [SerializeField] private StageUI _stageUI;
    [SerializeField] private TextMeshProUGUI _currentLevelText;
    
    private RectTransform _rect;
    
    private int _currentLevel;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();

        _currentLevel = PlayerData.GetLevel();
        Init(_currentLevel);
    }

    private void Init(int currentIndex)
    {
        _currentLevelText.text = "LEVEL " + (currentIndex + 1);
        _rect.anchoredPosition = new Vector2(0, 500);
    }

    public void LabelIntro()
    {
        _rect.DOAnchorPosY(-125, 1).SetEase(Ease.OutBack);
        var stageData = PlayerData.GetStageData();
        
        if(stageData is { Count: > 1 })
            _stageUI.gameObject.SetActive(true);
    }
    
    // public void LabelOutro()
    // {
    //     _rect.DOAnchorPosY(150, 1).SetEase(Ease.OutBack);
    // }
    
}