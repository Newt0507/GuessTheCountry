using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

//312 598
public class Score : MonoBehaviour
{
    private const int DEFAULT_CAMERA_SIZE = 4;
    
    [SerializeField] private float _speed = 7;
    
    private bool _isMoving;
    private float _camRatio;

    private bool _isFirstEnable = true;
    private float _delayTime;

    private void Awake()
    {
        _delayTime = SetDelayTimeByIndex();
    }

    // private void OnEnable()
    // {
    //     if (_isFirstEnable)
    //     {
    //         _isFirstEnable = false;
    //         return;
    //     };
    //
    //     GetPosition();
    //     var _camRatio = CameraController.Instance.camSizeAfterChoose / DEFAULT_CAMERA_SIZE;
    //     var _firstMovePosition = transform.position + (Vector3)Random.insideUnitCircle * _camRatio;
    //     transform.localScale = Vector2.one * _camRatio;
    //
    //     var sequence = DOTween.Sequence();
    //     sequence.Append(transform.DOMove(_firstMovePosition, .7f).SetEase(Ease.OutQuint));
    //
    //     Vector2 _endPosition = UIController.Instance.GetTopUI().GetScoreUI().GetScoreIconTransform().position;
    //
    //     sequence.Append(transform.DOMove(_endPosition, .7f).SetEase(Ease.Linear).SetDelay(_delayTime));
    //     sequence.OnComplete(Return);
    //     
    // }

    private void OnEnable()
    {
        if (_isFirstEnable)
        {
            _isFirstEnable = false;
            return;
        }

        GetPosition();
        _camRatio = CameraController.Instance.CamSizeAfterChoose / DEFAULT_CAMERA_SIZE;
        var _firstMovePosition = transform.position + (Vector3)Random.insideUnitCircle * _camRatio;
        
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(_firstMovePosition, .7f).SetEase(Ease.OutQuint));
        
        sequence.AppendInterval(_delayTime).OnComplete(() => _isMoving = true);
    }

    private void Update()
    {
        if (_isMoving)
        {
            Vector2 currentPosition = transform.position;
            Vector2 _endPosition = UIController.Instance.GetTopUI().GetScoreUI().GetScoreIconTransform().position;
            Vector2 newPosition =
                Vector2.MoveTowards(currentPosition, _endPosition, Time.deltaTime * _speed * _camRatio);
            transform.position = newPosition;

            if (Vector2.Distance(currentPosition, _endPosition) < 0.001f)
            {
                _isMoving = false;
                Return();
            }
        }
    }

    private void GetPosition()
    {
        var optionMenuUI = UIController.Instance.GetBottomUI().GetContainerUI().GetOptionMenuUI();
        for (int i = 0; i < optionMenuUI.GetOptions().Length; i++)
        {
            var answer = optionMenuUI.GetOptionAtIndex(i);
            if (answer.GetInfoPopupUI() == null) continue;

            transform.position = (Vector2)answer.transform.position;
            break;
        }
    }

    private float SetDelayTimeByIndex()
    {
        int index = 0;
        foreach (Transform child in ObjectPoolController.Instance.gameObject.transform)
        {
            if (child == gameObject) break;

            index++;
        }

        return index * 0.1f;
    }

    private void Return()
    {
        PlayerData.SetScore();
        AudioController.Instance.PlaySFX(ESound.Point);
        UIController.Instance.GetTopUI().GetScoreUI().GetScoreText();
        ObjectPoolController.Instance.Return(gameObject);
    }
}