using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    [SerializeField] private float _maxZoomSize;
    [SerializeField] private float _chooseZoomSize;
    [SerializeField] private float _paddingTarget;

    private Camera _mainCamera;
    public float CamSizeBeforeChoose { get; private set; }
    public float CamSizeAfterChoose { get; private set; }
    public Transform TargetTransform { get; private set; } 

    private Vector3 _defaultPosition;


    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }

        _mainCamera = GetComponent<Camera>();
        Init();
    }

    /// <summary>
    /// Initializes the camera position and size based on player data.
    /// </summary>
    private void Init()
    {
        Vector3 pos = PlayerData.GetCameraPosition();
        float size = PlayerData.GetCameraSize();

        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        _mainCamera.orthographicSize = size;
        _defaultPosition = new Vector3(0, 0, -10);
    }

    /// <summary>
    /// Calculates the new camera size based on the target's size and padding.
    /// </summary>
    /// <param name="target"></param>
    private void GetNewCamSize(Transform target)
    {
        var bounds = target.GetComponent<SpriteRenderer>().bounds;
        float targetWidth = bounds.size.x;
        float targetHeight = bounds.size.y;

        CamSizeBeforeChoose = Mathf.Max(targetWidth, targetHeight) * _paddingTarget / _mainCamera.aspect * 1.2f;
    }

    /// <summary>
    /// Calculates the vertical offset based on the camera size and UI position.
    /// </summary>
    /// <param name="camSize"></param>
    /// <returns></returns>
    private float OffsetY(float camSize)
    {
        return camSize * 2 * UIController.Instance.GetPlayAreaUI().rect().anchoredPosition.y / Screen.height;
    }

    /// <summary>
    /// Zooms the camera towards the target
    /// </summary>
    /// <param name="target"></param>
    /// <param name="duration"></param>
    private void ZoomIn(Transform target, float duration)
    {
        GetNewCamSize(target);
        Vector3 endPos = new Vector3(target.position.x, target.position.y - OffsetY(CamSizeBeforeChoose),
            transform.position.z);

        _mainCamera.transform.DOMove(endPos, duration);
        _mainCamera.DOOrthoSize(CamSizeBeforeChoose, duration);
    }

    /// <summary>
    /// Zooms the camera out to the maximum zoom size
    /// </summary>
    /// <param name="duration"></param>
    private void ZoomOutMax(float duration)
    {
        _mainCamera.transform.DOMove(_defaultPosition, duration).SetEase(Ease.InOutCubic);
        _mainCamera.DOOrthoSize(_maxZoomSize, duration).SetEase(Ease.InOutCubic);
    }

    /// <summary>
    /// Zooms out the camera with a specified duration
    /// </summary>
    /// <param name="duration"></param>
    public void ZoomOut(float duration)
    {
        CamSizeAfterChoose = _mainCamera.orthographicSize * (1 + _chooseZoomSize);
        _mainCamera.DOOrthoSize(CamSizeAfterChoose, duration);
    }

    /// <summary>
    /// Zooms the camera towards the specified target with the specified duration
    /// </summary>
    /// <param name="target"></param>
    /// <param name="duration"></param>
    /// <returns>IEnumerator</returns>
    public IEnumerator Zoom(Transform target, float duration)
    {
        TargetTransform = target;
        yield return new WaitForSeconds(1);
        if (_mainCamera.orthographicSize != _maxZoomSize)
        {
            ZoomOutMax(duration);
            yield return new WaitForSeconds(2);
        }

        ZoomIn(target, duration);
    }

    /// <summary>
    /// Returns the maximum zoom size of the camera
    /// </summary>
    /// <returns>float</returns>
    public float GetMaxZoomSize()
    {
        return _maxZoomSize;
    }
}