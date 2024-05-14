using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SwitchButtonUI : MonoBehaviour
{
    public enum SettingType
    {
        Sound,
        Vibration,
    }


    [SerializeField] private SettingType _type;
    [SerializeField] private Image _handle;
    [SerializeField] private Image _onBar;
    [SerializeField] private Sprite _on, _off;
    [Space]
    [SerializeField] private ButtonEffectLogic _button;

    private const int LEFT_POSITION_X = -35;
    private const int RIGHT_POSITION_X = 35;
    
    private bool _isMute;
    
    private void Awake()
    {
        _button.onClick.AddListener(ChangeVolume);
        Init();
    }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        switch (_type)
        {
            case SettingType.Sound:
                _isMute = PlayerData.IsSoundMuted();
                break;
            case SettingType.Vibration:
                _isMute = PlayerData.IsVibrationMute();
                break;
        }

        if (_isMute)
        {
            _onBar.fillAmount = 0;
            _handle.sprite = _off;
            _handle.rectTransform.anchoredPosition =
                new Vector2(LEFT_POSITION_X, _handle.rectTransform.anchoredPosition.y);
        }
        else
        {
            _onBar.fillAmount = 1;
            _handle.sprite = _on;
            _handle.rectTransform.anchoredPosition =
                new Vector2(RIGHT_POSITION_X, _handle.rectTransform.anchoredPosition.y);
        }
    }

    private void ChangeVolume()
    {
        _isMute = !_isMute;
        switch (_type)
        {
            case SettingType.Sound:
                AudioController.Instance.ToggleSound();
                break;
            case SettingType.Vibration:
                AudioController.Instance.ToggleVibration();
                break;
        }

        Visual();
    }

    private void Visual()
    {
        float duration = 0.2f;
        if (_isMute)
        {
            _onBar.DOFillAmount(0, duration).SetUpdate(true).SetEase(Ease.Linear);
            _handle.rectTransform.DOAnchorPosX(LEFT_POSITION_X, duration).SetUpdate(true)
                .OnComplete(() => { _handle.sprite = _off; });
        }
        else
        {
            _onBar.DOFillAmount(1, duration).SetUpdate(true).SetEase(Ease.Linear);
            _handle.rectTransform.DOAnchorPosX(RIGHT_POSITION_X, duration).SetUpdate(true)
                .OnComplete(() => { _handle.sprite = _on; });
        }
    }

}
