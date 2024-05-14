using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HardLevelPopupUI : MonoBehaviour
{
    private void Awake()
    {
        transform.localScale = Vector3.zero;
        Hide();
    }
    
    private void OnEnable()
    {
        transform.DOScale(Vector3.one, 1).SetEase(Ease.OutBack);
        StartCoroutine(IEShow());
    }

    private IEnumerator IEShow()
    {
        yield return new WaitForSeconds(2); 
        transform.DOScale(Vector3.zero, 1);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
