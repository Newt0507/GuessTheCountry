using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField] private PlayAreaUI _playAreaUI;
    [SerializeField] private TopUI _topUI;
    [SerializeField] private BottomUI _bottomUI;
    [SerializeField] private PopupUI _popupUI;
    [SerializeField] private LabelUI _labelUI;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public PlayAreaUI GetPlayAreaUI()
    {
        return _playAreaUI;
    }
    
    public TopUI GetTopUI()
    {
        return _topUI;
    }
    
    public BottomUI GetBottomUI()
    {
        return _bottomUI;
    }
    
    public PopupUI GetPopupUI()
    {
        return _popupUI;
    }

    public LabelUI GetLabelUI()
    {
        return _labelUI;
    }
}
