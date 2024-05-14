using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StageUI : MonoBehaviour
{
    [SerializeField] private Transform[] _stages;

    private RectTransform _rect;
    
    private void Awake()
    {
        _rect = GetComponent<RectTransform>();

        Init();
    }

    private void OnEnable()
    {
        VisualOptions();
    }

    private void Init()
    {
        gameObject.SetActive(false);
    }

    private void VisualOptions()
    {
        for (int i = 0; i < PlayerData.GetStageData().Count; i++)
        {
            if (PlayerData.GetPassedStage() > i)
                _stages[i].GetComponent<SingleStageUI>().SetColor();

            _stages[i].gameObject.SetActive(true);
        }
    }
}
