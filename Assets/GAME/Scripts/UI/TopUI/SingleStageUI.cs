using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleStageUI : MonoBehaviour
{
    [SerializeField] private Image _image;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void SetColor()
    {
        _image.color = Color.green;
    }
}
