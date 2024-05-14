using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SettingPopupUI : MonoBehaviour
{
    [SerializeField] private RectTransform _container;
    [SerializeField] private ButtonEffectLogic _closeButton;
    [SerializeField] private ButtonEffectLogic _resetGameButton;
    [SerializeField] private CanvasGroup _canvas;
    
    private void Awake()
    {
        _closeButton.onClick.AddListener(Cancel);
        _resetGameButton.onClick.AddListener(ShowResetGamePopup);
        
        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameController.Instance.PauseGame();
        _canvas.alpha = 1;
        _container.DOAnchorPosY(0, .75f).SetUpdate(true).SetEase(Ease.OutBack);
    }

    private void Cancel()
    {
        AudioController.Instance.PlaySFX(ESound.Click);
        _canvas.DOFade(0, .5f).SetUpdate(true).OnComplete(Hide);
        GameController.Instance.Resume();
    }

    private void OnDisable()
    {
        _container.anchoredPosition = new Vector2(0, -1500);
    }

    private void ShowResetGamePopup()
    {
        AudioController.Instance.PlaySFX(ESound.Click);
        UIController.Instance.GetPopupUI().GetResetGamePopupUI().gameObject.SetActive(true);
    }
}
