using System;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [SerializeField] private CountryDatas _countryDatas;

    [Space] [SerializeField] private Transform _levelContainer;
    [SerializeField] private Transform _resultContainer;
    [SerializeField] private Color _currentLevelColor;

    [Space] [SerializeField] private float _duration;

    public const int _timerMax = 10; // Stores the maximum value for the game timer

    public int _timer { get; private set; } // Holds the current value of the game timer

    private IEnumerator _IECountdown; // store coroutine for game countdown

    private int _currentStage;
    private int _currentLevel;
    private int _lastStage;
    private int _passedStage;

    private SpriteRenderer[] _lastFlags;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;

        //Ensure that the game play normally
        Time.timeScale = 1;

        //Get data
        _currentStage = PlayerData.GetStage();
        _currentLevel = PlayerData.GetLevel();
        _lastStage = PlayerData.GetLastStage();
        _passedStage = PlayerData.GetPassedStage();
        _timer = _timerMax;
        Visual(_currentLevel);
    }

    private void Start()
    {
        //AudioController.Instance.PlayMusic(ESound.Background);
        StartCoroutine(PrepareToStartStage(_currentStage, _lastStage));
    }

    /// <summary>
    /// Handles the visual representation of flags for completed levels
    /// </summary>
    /// <param name="level"></param>
    private void Visual(int level)
    {
        for (int i = 0; i < level; i++)
        {
            Transform flagPrefab = _countryDatas.flagDatas.datas[i].prefab;
            Vector3 pos = _countryDatas.maps[i].position;

            var oldFlag = Instantiate(flagPrefab, pos, Quaternion.identity, _resultContainer);

            var mask = oldFlag.GetComponent<MaskUI>();
            mask.SetMaskOrder(i);
            mask.SetSpriteOrder(i);

            if ((PlayerData.GetLevel() + 1) % 5 == 0 && PlayerData.GetStageData().Contains(i))
            {
                if (_passedStage > 0) _passedStage--;
                else oldFlag.gameObject.SetActive(false);
            }

            if (i == _lastStage)
            {
                _lastFlags = oldFlag.GetComponentsInChildren<SpriteRenderer>();

                foreach (var flag in _lastFlags)
                {
                    var color = flag.color;
                    color = new Color(color.r, color.g, color.b, 0);
                    flag.color = color;
                }
                
            }
        }
    }


    /// <summary>
    /// Prepare to start a new stage
    /// </summary>
    /// <param name="stage"></param>
    /// <param name="lastStage"></param>
    /// <returns></returns>
    private IEnumerator PrepareToStartStage(int stage, int lastStage)
    {
        //Show old flag
        if (PlayerData.GetCameraSize() != CameraController.Instance.GetMaxZoomSize() && lastStage >= 0)
        {
            yield return new WaitForSeconds(1);
            //AudioController.Instance.PlaySFX(ESound.ShowOldMap);

            foreach (var flag in _lastFlags)
            {
                flag.DOFade(1, _duration);
            }

            yield return new WaitForSeconds(1);
            UIController.Instance.GetPlayAreaUI().GetTextWriterUI();
            yield return new WaitForSeconds(3.5f);
        }        

        //Show "Guess the country" text
        yield return new WaitForSeconds(1);
        UIController.Instance.GetPlayAreaUI().GetGuessTheCountryUI().gameObject.SetActive(true);
        yield return new WaitForSeconds(.7f);
        
        //Show Hard level popup
        if ((PlayerData.GetLevel() + 1) % 5 == 0)
        {
            yield return new WaitForSeconds(1.5f);
            AudioController.Instance.PlaySFX(ESound.Warning);
            UIController.Instance.GetPopupUI().GetHardLevelUI().gameObject.SetActive(true);
        }

        //Show Label
        yield return new WaitForSeconds(1);
        UIController.Instance.GetLabelUI().LabelIntro();

        AudioController.Instance.PlayMusic(ESound.Background);
        
        //Zoom camera
        StartCoroutine(CameraController.Instance.Zoom(_levelContainer.GetChild(stage), _duration));

        //if camera does not zoom out from old flag, just zoom to next flag (player just choose wrong flag last stage)
        //-> zoom time = 3.5f
        //else zoom time = 3.5f + 2 = 5.5f
        var zoomFinishTime = 3.5f;
        if (PlayerData.GetCameraSize() != CameraController.Instance.GetMaxZoomSize()) zoomFinishTime += 2; 
        yield return new WaitForSeconds(zoomFinishTime);
        
        //Highlight the current stage and visual UI
        HighlightCurrentStage(stage);
        UIController.Instance.GetBottomUI().GetContainerUI().ShowUI(_duration);
    }

    /// <summary>
    /// Displays the map for the current stage by changing color to "_currentLevelColor"
    /// </summary>
    /// <param name="stage"></param>
    private void HighlightCurrentStage(int stage)
    {
        _levelContainer.GetChild(stage).GetComponent<SpriteRenderer>().DOColor(_currentLevelColor, _duration);
    }

    /// <summary>
    /// Starts the countdown timer
    /// </summary>
    public void StartCountdown()
    {
        _IECountdown = IECountdown();
        StartCoroutine(_IECountdown);
    }

    /// <summary>
    /// Stops the countdown timer
    /// </summary>
    public void StopCountdown()
    {
        if(_IECountdown!= null) StopCoroutine(_IECountdown);
    }

    /// <summary>
    /// Manages the countdown timer for the game
    /// </summary>
    /// <returns></returns>
    private IEnumerator IECountdown()
    {
        while (_timer > 0)
        {
            if(UIController.Instance.GetPopupUI().GetInfoUI().gameObject.activeInHierarchy) yield break;
            
            yield return new WaitForSeconds(1);

            AudioController.Instance.PlaySFX(ESound.Tick);
            _timer -= 1;

            if (_timer <= 0)
            {
                StartCoroutine(IETimeOutVisual());
            }
        }
    }

    /// <summary>
    /// Reloads the current scene
    /// </summary>
    private void LoadScene()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Initiates the game over sequence
    /// </summary>
    public void GameOver()
    {
        StartCoroutine(IEGameOver());
    }

    /// <summary>
    /// Handles the game over sequence
    /// </summary>
    /// <returns></returns>
    private IEnumerator IEGameOver()
    {
        yield return new WaitForSeconds(1.5f);
        PlayerData.ResetPassedStage();
        PlayerData.SetDefaultCamera();
        LoadScene();
    }

    /// <summary>
    /// Displays visual effects when the game timer runs out
    /// </summary>
    /// <returns></returns>
    private IEnumerator IETimeOutVisual()
    {
        yield return new WaitForSeconds(1.5f);
        AudioController.Instance.PlaySFX(ESound.Lose);
        UIController.Instance.GetPlayAreaUI().GetTimeOutUI().gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        GameOver();
    }

    /// <summary>
    /// Pauses the game
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// Resumes the game
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1;
    }

    /// <summary>
    /// Update next stage data and reload the current scene 
    /// </summary>
    public void NextLevel()
    {
        PlayerData.UpdateStage();
        LoadScene();
    }

    /// <summary>
    /// Resets the game
    /// </summary>
    public void ResetGame()
    {
        PlayerData.ResetData();
        Resume();
        LoadScene();
    }
}