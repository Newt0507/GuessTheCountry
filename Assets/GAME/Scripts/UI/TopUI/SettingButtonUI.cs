using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingButtonUI : MonoBehaviour
{
    [SerializeField] private ButtonEffectLogic _button;

    private void Awake()
    {
        _button.onClick.AddListener(Settings);
    }

    private void Settings()
    {
        AudioController.Instance.PlaySFX(ESound.Click);
        UIController.Instance.GetPopupUI().GetSettingPopupUI().gameObject.SetActive(true);
    }
}
