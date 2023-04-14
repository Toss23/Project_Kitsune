using System;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    public event Action<float, float> OnActive;
    public event Action<bool> IsActive;

    [SerializeField] private GameObject _content;
    [SerializeField] private Transform _stick;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private int _maxRadiusStick = 200;

    private Vector2 _screenSize;
    private Vector2 _screenSizePreset;
    private Vector2 _joystickPosition;
    private Vector2 _stickPosition;
    private float _angle;

    private void Awake()
    {
        _screenSize = new Vector2(Screen.width, Screen.height);
        _screenSizePreset = new Vector2(1920, 1080);
        _content.SetActive(false);
    }

    private void Update()
    {
        _stickPosition = Vector3.zero;

        if (Application.isMobilePlatform)
        {
            // Touch control
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _content.SetActive(true);
                        _joystickPosition = TouchPositionOnCanvas(touch.position);
                        transform.localPosition = _joystickPosition;
                        break;
                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        GetStickPositionAndAngle(touch.position, out _stickPosition, out _angle);
                        OnActive?.Invoke(_angle, Time.deltaTime);
                        IsActive?.Invoke(true);
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        _content.SetActive(false);
                        _stickPosition = Vector2.zero;
                        IsActive?.Invoke(false);
                        break;
                }
            }
        }
        else
        {
            // Mouse control
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _content.SetActive(true);
                _joystickPosition = TouchPositionOnCanvas(Input.mousePosition);
                transform.localPosition = _joystickPosition;
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                GetStickPositionAndAngle(Input.mousePosition, out _stickPosition, out _angle);
                OnActive?.Invoke(_angle, Time.deltaTime);
                IsActive?.Invoke(true);
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                _content.SetActive(false);
                _stickPosition = Vector2.zero;
            }

            // Keyboard control
            if (Input.GetKey(KeyCode.W))
            {
                _stickPosition = new Vector3(0, _maxRadiusStick);
                _angle = 90;
                OnActive?.Invoke(_angle, Time.deltaTime);
                IsActive?.Invoke(true);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                _stickPosition = new Vector3(0, -_maxRadiusStick);
                _angle = -90;
                OnActive?.Invoke(_angle, Time.deltaTime);
                IsActive?.Invoke(true);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                _stickPosition = new Vector3(-_maxRadiusStick, 0);
                _angle = 180;
                OnActive?.Invoke(_angle, Time.deltaTime);
                IsActive?.Invoke(true);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _stickPosition = new Vector3(_maxRadiusStick, 0);
                _angle = 0;
                OnActive?.Invoke(_angle, Time.deltaTime);
                IsActive?.Invoke(true);
            }

            if (!Input.anyKey)
            {
                IsActive?.Invoke(false);
            }
        }

        _stick.localPosition = Vector2.ClampMagnitude(_stickPosition, _maxRadiusStick);
    }

    private void GetStickPositionAndAngle(Vector2 touchPosition, out Vector2 stickPosition, out float angle)
    {
        stickPosition = TouchPositionOnCanvas(touchPosition) - _joystickPosition;
        angle = Mathf.Atan2(stickPosition.y, stickPosition.x) * Mathf.Rad2Deg;
    }

    public Vector2 TouchPositionOnCanvas(Vector2 touchPosition)
    {
        return new Vector3((touchPosition.x / _screenSize.x - 0.5f) * _screenSizePreset.x, (touchPosition.y / _screenSize.y - 0.5f) * _screenSizePreset.y, 0);
    }
}