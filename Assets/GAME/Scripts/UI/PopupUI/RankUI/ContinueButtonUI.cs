using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ContinueButtonUI : MonoBehaviour
{
    [SerializeField] private RectTransform _rect;
    [SerializeField] private CanvasGroup _canvas;
    [SerializeField] private ButtonEffectLogic _button;

    [Space] [SerializeField] private RankPopupUI _rankPopupUI;


    private void Awake()
    {
        Init();

        _button.onClick.AddListener(Hide);
    }

    private void Init()
    {
        //_rect.localScale = Vector3.zero;
        //_rect.anchoredPosition = new Vector2(0, -1000);
        _canvas.alpha = 0;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        float duration = 1;
        //_rect.DOScale(Vector3.one, duration).SetDelay(1);
        //_rect.DOAnchorPosY(-430, duration).SetDelay(.5f);
        _canvas.DOFade(1, duration);
    }


    private void Hide()
    {
        AudioController.Instance.PlaySFX(ESound.Click);
        _rankPopupUI.GetCanvasGroup().DOFade(0, .5f);
        //GameController.Instance.Resume();
        GameController.Instance.NextLevel();
    }
}
