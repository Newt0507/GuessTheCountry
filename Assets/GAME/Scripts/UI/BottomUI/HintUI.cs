using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hintText;
    [SerializeField] private ButtonEffectLogic _button;
        
    private void Awake()
    {
        GetHint();
        _button.onClick.AddListener(Hint);
    }

    private void GetHint()
    {
        _hintText.text = PlayerData.GetHint().ToString();
    }

    private void Hint()
    {
        AudioController.Instance.PlaySFX(ESound.Hint);
        int _hintNumber = PlayerData.GetHint();
        if (_hintNumber > 0)
        {
            PlayerData.SetHint(-1);
            GetHint();
            GetAnswer();
        }
        else
        {
            //Show ad 
            Debug.Log("No more hint");
        }
    }

    private void GetAnswer()
    {
        var optionMenuUI = UIController.Instance.GetBottomUI().GetContainerUI().GetOptionMenuUI();
        for (int i = 0; i < optionMenuUI.GetOptions().Length; i++)
        {
            var answer = optionMenuUI.GetOptionAtIndex(i);
            if(answer.GetInfoPopupUI() == null) continue;
            
            answer.ShowPopupUI();
            break;
        }
    }
}
