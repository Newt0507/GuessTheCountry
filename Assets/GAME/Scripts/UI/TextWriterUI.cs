using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextWriterUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvas;
    [Space]
    [SerializeField] private TextMeshProUGUI _textWriter;
    [SerializeField] private float _timePerCharacter;

    private const int DEFAULT_CAMERA_SIZE = 4;
    
    private float _timer;
    private int _characterIndex;

    private void Awake()
    {
        _textWriter.text = "";
        _canvas.alpha = 0;
        gameObject.SetActive(false);
    }

    public void Show(string population, int populationRank, Vector3 position)
    {
        var OffsetY = .5f * PlayerData.GetCameraSize() / DEFAULT_CAMERA_SIZE;
        
        transform.position = new Vector3(position. x, position.y + OffsetY);
        
        _canvas.DOFade(1, 1);
        
        var text = "Population: " + population;
        text += "\nRank: " + populationRank;

        _characterIndex = 0;
        
        StartCoroutine(IEShow(text));
    }
    
    private IEnumerator IEShow(string textToWrite)
    {
        while (true)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            _timer -= Time.deltaTime;
            if (_timer > 0) continue;
            _timer += _timePerCharacter;
            _characterIndex++;

            var text = textToWrite[.._characterIndex];
            text += "<color=#00000000>" + textToWrite[_characterIndex..] + "</color>";
            _textWriter.text = text;

            if (_characterIndex < textToWrite.Length) continue;
            
            yield return new WaitForSeconds(1.2f);
            _canvas.DOFade(0, 1);
            yield break;
        }
    }
}