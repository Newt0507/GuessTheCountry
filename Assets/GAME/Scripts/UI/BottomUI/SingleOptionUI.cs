using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SingleOptionUI : MonoBehaviour
{
    [SerializeField] private Image _flag;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private ButtonEffectLogic _button;

    [SerializeField] private Transform _bubbleEffects;
    [SerializeField] private Transform _crossEffects;

    private InfoPopupUI _infoPopupUI;
    private CanvasGroup _canvas;

    private bool _firstClick = true;

    private void Awake()
    {
        _canvas = GetComponentInParent<CanvasGroup>();
        _button.onClick.AddListener(ShowPopupUI);

        HideEffect();
    }

    public void ShowPopupUI()
    {
        if(GameController.Instance._timer <= 0) return;
        if (_canvas.alpha < 0.5f) return;
        if (!_firstClick) return;
        
        _firstClick = false;
        GameController.Instance.StopCountdown();
        AudioController.Instance.StopMusic();

        if (_infoPopupUI != null)
        {
            AudioController.Instance.PlaySFX(ESound.ChooseRight);
            CameraController.Instance.ZoomOut(1);
            _bubbleEffects.gameObject.SetActive(true);
            _name.color = Color.green;

            //PlayerData.UpdateStage();
            //StartCoroutine(ShowScore());
            ShowScore();
        }
        else
        {
            AudioController.Instance.PlaySFX(ESound.Lose);
            AudioController.Instance.PlayVibration();
            _crossEffects.gameObject.SetActive(true);
            _name.color = Color.red;

            GameController.Instance.GameOver();
            if((PlayerData.GetLevel() + 1) % 5 == 0) PlayerData.ResetStageOfCurrentLevel();
        }

        HideOtherAnswer();
        UIController.Instance.GetBottomUI().GetContainerUI().HideUI(2);
    }

    //private IEnumerator ShowScore()
    private void ShowScore()
    {
        //yield return new WaitForSeconds(1);
        //pool score
        ObjectPoolController.Instance.GetAll("Score");
        //ObjectPoolController.Instance.GetAll("ScoreTransform");

        if (PlayerData.GetStage() < PlayerData.GetLevel())
            StartCoroutine(IENexTLevel());
        else
            StartCoroutine(IEShowAnswer());
    }
    
    private void HideOtherAnswer()
    {
        foreach (Transform opt in transform.parent)
        {
            if (opt.gameObject == gameObject) continue;
            foreach (Transform child in opt)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator IENexTLevel()
    {
        yield return new WaitForSeconds(2.5f);
        GameController.Instance.NextLevel();        
    }
    
    private IEnumerator IEShowAnswer()
    {
        yield return new WaitForSeconds(2);
        _infoPopupUI.gameObject.SetActive(true);
    }

    private void HideEffect()
    {
        _bubbleEffects.gameObject.SetActive(false);
        _crossEffects.gameObject.SetActive(false);
    }

    public void SetProperties(Sprite flag, string name)
    {
        _flag.sprite = flag;
        _name.text = name;
    }

    public InfoPopupUI GetInfoPopupUI()
    {
        return _infoPopupUI;
    }
    
    public void SetAnswerPopupUI(InfoPopupUI infoPopupUI)
    {
        _infoPopupUI = infoPopupUI;
    }
    
}