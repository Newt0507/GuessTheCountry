using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

public class ImageUI : MonoBehaviour
{
    [SerializeField] private RectTransform[] _images;
    [SerializeField] private NextCountryUI _nextCountryUI;
    
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _images[0].anchoredPosition = new Vector2(-900, 650);
        _images[1].anchoredPosition = new Vector2(-900, -650);
        _images[2].anchoredPosition = new Vector2(900, 650);
        _images[3].anchoredPosition = new Vector2(900, -650);
        
        _images[4].localScale = Vector3.zero;
        _images[4].localRotation = Quaternion.Euler(Vector3.zero);
    }

    public void Show()
    {
        Sequence sequence = DOTween.Sequence();

        var duration = .7f;

        sequence.Append(_images[0].DOAnchorPos(new Vector2(-148, 128), duration).SetDelay(2));
        sequence.Append(_images[1].DOAnchorPos(new Vector2(-145, -115), duration));
        sequence.Append(_images[2].DOAnchorPos(new Vector2(144, 125), duration));
        sequence.Append(_images[3].DOAnchorPos(new Vector2(143, -120), duration));
        sequence.AppendInterval(.5f);
        sequence.Append(_images[4].DOScale(Vector3.one, duration).SetEase(Ease.OutBack))
            .Join(_images[4].DOLocalRotate(new Vector3(0, 0, 360), duration, RotateMode.FastBeyond360));

        sequence.AppendInterval(1.5f);
        sequence.OnComplete(() => _nextCountryUI.gameObject.SetActive(true));
    }


    public void SetImages(Sprite[] images)
    {
        for (int i = 0; i < images.Length; i++)
        {
            _images[i].GetComponent<SingleImageUI>().SetImage(images[i]);
        }
    }
}
