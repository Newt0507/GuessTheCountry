using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private Image _timerInnerImage;
    [SerializeField] private Image _timerOuterImage;
    [SerializeField] private TextMeshProUGUI _timerText;

    [SerializeField] private float _duration;

    private CanvasGroup _canvas;
    private Animator _anim;

    private bool _start = false;

    private void Awake()
    {
        _canvas = GetComponent<CanvasGroup>();
        _anim = GetComponentInChildren<Animator>();
        
        _canvas.alpha = 0;
        _anim.enabled = false;
    }

    private void Start()
    {
        StartCoroutine(IECountdown(_duration));
    }

    private IEnumerator IECountdown(float duration)
    {
        while (true)
        {
            if (!_start && _canvas.alpha > 0)
            {
                _start = true;
                GameController.Instance.StartCountdown();
            }

            int remainTime = GameController.Instance._timer;
            int maxTime = GameController._timerMax;
            SetTimer(remainTime, maxTime, remainTime.ToString(), duration);

            ChangeColorByTimer(remainTime, duration);

            if (remainTime < 5) ScaleUI();
            
            yield return new WaitForSeconds(1);
        }
    }

    private void SetTimer(int timer, int max, string timerText, float duration)
    {
        float ratioTime = (float)timer / max;

        _timerOuterImage.DOFillAmount(ratioTime, duration);
        _timerInnerImage.DOFillAmount(ratioTime, duration);
        _timerText.text = timerText;
    }

    private void ChangeColorByTimer(float timer, float duration)
    {
        Color color = Color.white;
        if (timer < 4)
            color = Color.red;
        else if (timer < 7)
            color = Color.yellow;
        else
            color = Color.green;

        _timerInnerImage.DOColor(color, duration);
    }

    private void ScaleUI()
    {
        _anim.enabled = true;
    }

}
