using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ResetGamePopupUI : MonoBehaviour
{
    [SerializeField] private RectTransform _container;
    [SerializeField] private ButtonEffectLogic _closeButton;
    [SerializeField] private ButtonEffectLogic _resetGameButton;
    [SerializeField] private CanvasGroup _canvas;

    private void Awake()
    {
        _closeButton.onClick.AddListener(Hide);
        _resetGameButton.onClick.AddListener(ResetGame);

        StartPosition();
    }

    private void StartPosition()
    {
        _container.anchoredPosition = new Vector2(0, -1500);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _canvas.alpha = 1;
        _container.DOAnchorPosY(0, .75f).SetUpdate(true).SetEase(Ease.OutBack);
    }

    private void Hide()
    {
        AudioController.Instance.PlaySFX(ESound.Click);
        _canvas.DOFade(0, .5f).SetUpdate(true).OnComplete(StartPosition);
    }

    private void ResetGame()
    {
        AudioController.Instance.PlaySFX(ESound.Click);
        GameController.Instance.ResetGame();
    }
}
