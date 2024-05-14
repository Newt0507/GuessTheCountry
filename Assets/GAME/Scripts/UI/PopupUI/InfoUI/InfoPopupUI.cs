using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InfoPopupUI : MonoBehaviour
{
    [SerializeField] private CountryDatas _countryDatas;

    [Space] [SerializeField] private ImageUI _imageUI;
    [SerializeField] private DetailUI _detailUI;
    //[SerializeField] private NextCountryUI _nextCountryUI;

    [Space] [SerializeField] private CanvasGroup _canvas;

    private int _currentStage;

    private void Awake()
    {
        _canvas.alpha = 0;
        _currentStage = PlayerData.GetStage();
        Init(_currentStage);
        gameObject.SetActive(false);
    }


    private void Init(int stage)
    {
        _imageUI.SetImages(_countryDatas.flagDatas.datas[stage].imgs);
        _detailUI.SetFlag(_countryDatas.flagDatas.datas[stage].flag);
        _detailUI.SetName(_countryDatas.flagDatas.datas[stage].name);
        _detailUI.SetInfoText(_countryDatas.flagDatas.datas[stage].info);
    }

    private void OnEnable()
    {
        _canvas.DOFade(1, 1);
        _imageUI.Show();
        _detailUI.Show();
        //_nextCountryUI.Show();
    }
}