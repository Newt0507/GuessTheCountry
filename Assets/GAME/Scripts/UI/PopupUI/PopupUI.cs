using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PopupUI : MonoBehaviour
{
    [SerializeField] private InfoPopupUI _infoPopupUI;
    [SerializeField] private HardLevelPopupUI _hardLevelPopupUI;
    [SerializeField] private SettingPopupUI _settingPopupUI;
    [SerializeField] private ResetGamePopupUI _resetGamePopupUI;
    [SerializeField] private RankPopupUI _rankPopupUI;

    public InfoPopupUI GetInfoUI()
    {
        return _infoPopupUI;
    }
    
    public HardLevelPopupUI GetHardLevelUI()
    {
        return _hardLevelPopupUI;
    }
    
    public SettingPopupUI GetSettingPopupUI()
    {
        return _settingPopupUI;
    }
    
    public ResetGamePopupUI GetResetGamePopupUI()
    {
        return _resetGamePopupUI;
    }
    
    public RankPopupUI GetRankPopupUI()
    {
        return _rankPopupUI;
    }
}
