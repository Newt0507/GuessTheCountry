using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayAreaUI : MonoBehaviour
{
    [SerializeField] private CountryDatas _countryDatas;
    
    [SerializeField] private TimeOutUI _timeOutUI;
    [SerializeField] private ReadyUI _readyUI;
    [SerializeField] private GuessTheCountryUI _guessTheCountryUI;
    [SerializeField] private TextWriterUI _textWriterUI;

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
        {
            _rect.anchoredPosition = new Vector2(0, 200);
            _rect.sizeDelta = new Vector2(0, 630);
        }
        else
        {
            _rect.anchoredPosition = new Vector2(0, 255);
            _rect.sizeDelta = new Vector2(0, 510);
        }
    }

    public TimeOutUI GetTimeOutUI()
    {
        return _timeOutUI;
    }
    
    public ReadyUI GetReadyUI()
    {
        return _readyUI;
    }
    
    public GuessTheCountryUI GetGuessTheCountryUI()
    {
        return _guessTheCountryUI;
    }

    public void GetTextWriterUI()
    {
        var population = _countryDatas.flagDatas.datas[PlayerData.GetLastStage()].population;
        var populationRank = _countryDatas.flagDatas.datas[PlayerData.GetLastStage()].populationRank;
        var position = _countryDatas.maps[PlayerData.GetLastStage()].position;

        _textWriterUI.gameObject.SetActive(true);
        _textWriterUI.Show(population, populationRank, position);
    }

}
