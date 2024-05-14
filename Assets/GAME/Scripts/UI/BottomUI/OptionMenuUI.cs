using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class OptionMenuUI : MonoBehaviour
{
    [SerializeField] private CountryDatas _countryDatas;
    
    [SerializeField] private Transform[] _options;

    private CanvasGroup _canvas;
    private GridLayoutGroup _grid;

    private int _currentStage;
    private int _optionNumber;

    private void Awake()
    {
        _grid = GetComponent<GridLayoutGroup>();
        _canvas = GetComponent<CanvasGroup>();
        
        _canvas.alpha = 0;
        _currentStage = PlayerData.GetStage();
        _optionNumber = _countryDatas.maps[_currentStage].options.Count;
        Hide();
    }

    private void Start()
    {
        VisualOptions(_currentStage, _optionNumber);
        InitOptionsUI(_optionNumber);
    }


    private void VisualOptions(int stage, int optionNumber)
    {
        for (int i = 0; i < optionNumber; i++)
        {
            int optionIndex = _countryDatas.maps[stage].options[i];
            Sprite flag = _countryDatas.flagDatas.datas[optionIndex].flag;
            string name = _countryDatas.flagDatas.datas[optionIndex].name;

            SingleOptionUI opt = _options[i].GetComponent<SingleOptionUI>();
            opt.SetProperties(flag, name);

            if (optionIndex == stage)
                opt.SetAnswerPopupUI(UIController.Instance.GetPopupUI().GetInfoUI());

            opt.gameObject.SetActive(true);
        }
    }

    private void InitOptionsUI(int option)
    {
        if (option == 3)
        {
            _grid.constraintCount = 3;
            _grid.spacing = new Vector2(30, 0);
        }
        else
        {
            _grid.constraintCount = 2;
            _grid.spacing = new Vector2(90, 0);
        }
    }

    private void Hide()
    {
        foreach (Transform opt in _options)
        {
            opt.gameObject.SetActive(false);
        }
    }

    public SingleOptionUI GetOptionAtIndex(int index)
    {
        return _options[index].GetComponent<SingleOptionUI>();
    }

    public Transform[] GetOptions()
    {
        return _options;
    }
}
