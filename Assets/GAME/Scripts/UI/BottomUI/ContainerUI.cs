using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ContainerUI : MonoBehaviour
{
    [SerializeField] private CountryDatas _countryDatas;

    [SerializeField] private RectTransform _backgroundUI;
    [SerializeField] private RectTransform _optionMenuUI;
    [SerializeField] private RectTransform _adButtonUI;
    [SerializeField] private RectTransform _hintButtonUI;
    [SerializeField] private RectTransform _timerUI;
    
    private int _currentStage;
    private RectTransform _rect;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _currentStage = PlayerData.GetStage();

        Init(_countryDatas.maps[_currentStage].options.Count);
    }

    private void Init(int option)
    {
        if (option == 3)
            _rect.offsetMax = new Vector2(0, 385);
        else
            _rect.offsetMax = new Vector2(0, 500);

        _adButtonUI.anchoredPosition = new Vector2(-200, 0);
        _hintButtonUI.anchoredPosition = new Vector2(200, 0);
        _timerUI.anchoredPosition = new Vector2(0, -100);
    }

    public void ShowUI(float duration)
    {
        UIController.Instance.GetPlayAreaUI().GetReadyUI().gameObject.SetActive(true);
        _backgroundUI.GetComponent<CanvasGroup>().DOFade(1, duration)
            .OnComplete(() =>
            {
                float moveDuration = duration / 3;
                _adButtonUI.DOAnchorPosX(0, moveDuration);
                _hintButtonUI.DOAnchorPosX(0, moveDuration);
                _timerUI.DOAnchorPosY(100, moveDuration);
                _optionMenuUI.GetComponent<CanvasGroup>().DOFade(1, duration);
                _timerUI.GetComponent<CanvasGroup>().DOFade(1, duration);
            });
    }

    public void HideUI(float duration)
    {
        float moveDuration = duration / 3;
        _adButtonUI.DOAnchorPosX(-200, moveDuration);
        _hintButtonUI.DOAnchorPosX(200, moveDuration);
    }

    public OptionMenuUI GetOptionMenuUI()
    {
        return _optionMenuUI.GetComponent<OptionMenuUI>();
    }
    
}
