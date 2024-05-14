using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleImageUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    
    public void SetImage(Sprite sprite)
    {
        _image.sprite = sprite;
    }
    
}
