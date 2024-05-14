using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdUI : MonoBehaviour
{
    [SerializeField] private ButtonEffectLogic _button;

    private void Awake()
    {
        _button.onClick.AddListener(Ad);
    }

    private void Ad()
    {
        
    }
}
