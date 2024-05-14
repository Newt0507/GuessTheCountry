using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Image _scoreImage;

    private void Awake()
    {
        GetScoreText();
    }

    private void OnEnable()
    {
        GetScoreText();
    }

    public void GetScoreText()
    {
        _scoreText.text = PlayerData.GetScore().ToString();
    }

    public Transform GetScoreIconTransform()
    {
        return _scoreImage.transform;
    }
}
