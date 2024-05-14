using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RankPopupUI : MonoBehaviour
{
    [SerializeField] private List<SingleRankUI> _rankListUI;
    [SerializeField] private List<Sprite> _flagList;
    [SerializeField] private List<string> _nameList;
    [SerializeField] private Sprite _baseFlag, _yourRankBg, _otherBg;
    [SerializeField] private Image _fillScrollbarImage;
    [SerializeField] private Scrollbar _scrollbar;
    [SerializeField] private SingleRankUI _yourRankUI;
    [Space]
    [SerializeField] private CanvasGroup _canvas;
    [SerializeField] private ContinueButtonUI _continueButton;
    
    private int _startId;
    private int _currentRank;

    private void Awake()
    {
        Shuffle(_flagList);
        Shuffle(_nameList);
        
        _currentRank = PlayerData.GetRank();
        
        _canvas.alpha = 0;
        gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        AudioController.Instance.PlaySFX(ESound.Rank);
        _startId = _currentRank - 3;

        _canvas.DOFade(1, .5f);
        Show();
    }
    
    private void Show()
    {
        _rankListUI[3].gameObject.SetActive(false);
        _yourRankUI.gameObject.SetActive(true);
        
        _fillScrollbarImage.fillAmount = 0;
        _scrollbar.value = 0;

        for (int i = 0; i < _rankListUI.Count; i++)
        {
            _rankListUI[i].SetID(_startId + i);
            _rankListUI[i].SetFlag(_flagList[i]);
            _rankListUI[i].SetBg(_otherBg);
            _rankListUI[i].SetName(_nameList[i]);
        }

        _rankListUI[3].SetFlag(GetCurrentCountrySprite());
        _rankListUI[3].SetBg(_yourRankBg);
        _rankListUI[3].SetName("You");
        
        InvokeRepeating(nameof(ChangeRank), 0.1f, 0.1f);

        // _fillScrollbarImage.DOFillAmount(1, 1.5f)
        //     .OnUpdate(() => _scrollbar.value = _fillScrollbarImage.fillAmount)
        //     .OnComplete(() =>_rankListUI[3].gameObject.SetActive(true))    
        
        Sequence sequence = DOTween.Sequence();

        sequence.Append(_fillScrollbarImage.DOFillAmount(1, 1.5f)
            .OnUpdate(() => _scrollbar.value = _fillScrollbarImage.fillAmount)
            .OnComplete(() => _rankListUI[3].gameObject.SetActive(true)));   

        sequence.AppendInterval(1.5f);
        sequence.OnComplete(() =>_continueButton.gameObject.SetActive(true));
        
    }

    private Sprite GetCurrentCountrySprite()
    {
        var countryCode = RegionInfo.CurrentRegion.Name;
        foreach (var flag in _flagList)
        {
            if(flag.name == countryCode)
                return flag;
        }

        return _baseFlag;
    }

    private void ChangeRank()
    {
        _yourRankUI.SetID(_startId + 20);
        _startId--;

        if (_startId + 20 == _currentRank)
        {
            CancelInvoke(nameof(ChangeRank));
            PlayerData.SetRank();
            _yourRankUI.gameObject.SetActive(false);
            //Time.timeScale = 0;
        }
    }
    
    
    private void Shuffle<T>(List<T> list)
    {
        var count = list.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = Random.Range(i, count);
            (list[i], list[r]) = (list[r], list[i]);
        }
    }

    public CanvasGroup GetCanvasGroup()
    {
        return _canvas;
    }
}