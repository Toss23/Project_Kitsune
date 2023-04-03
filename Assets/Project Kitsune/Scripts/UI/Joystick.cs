using System;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    public event Action<float, float> OnActive;
    public event Action<bool> IsActive;

    [SerializeField] private Transform _stick;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private int _maxRadiusStick = 200;

    private Vector3 _screenSize;
    private Vector3 _joystickPosition;
    private float _angle;

    private void Awake()
    {
        float width = Screen.width / _canvas.scaleFactor;
        float height = Screen.height / _canvas.scaleFactor;
        _screenSize = new Vector3(width, height);
        _joystickPosition = transform.localPosition;
    }

    private void Update()
    {
        Vector3 stickPosition = Vector3.zero;

        if (Application.isMobilePlatform)
        {
            // Touch control
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        GetStickPositionAndAngle(touch.position, out stickPosition, out _angle);
                        OnActive?.Invoke(_angle, Time.deltaTime);
                        IsActive?.Invoke(true);
                        break;
                    case TouchPhase.Ended:
                        IsActive?.Invoke(false);
                        break;
                }
            }
        }
        else
        {
            // Mouse control
            if (Input.GetKey(KeyCode.Mouse0))
            {
                GetStickPositionAndAngle(Input.mousePosition, out stickPosition, out _angle);
                OnActive?.Invoke(_angle, Time.deltaTime);
                IsActive?.Invoke(true);
            }

            // Keyboard control
            if (Input.GetKey(KeyCode.W))
            {
                stickPosition = new Vector3(0, _maxRadiusStick);
                _angle = 90;
                OnActive?.Invoke(_angle, Time.deltaTime);
                IsActive?.Invoke(true);
            }

            if (Input.GetKey(KeyCode.S))
            {
                stickPosition = new Vector3(0, -_maxRadiusStick);
                _angle = -90;
                OnActive?.Invoke(_angle, Time.deltaTime);
                IsActive?.Invoke(true);
            }

            if (Input.GetKey(KeyCode.A))
            {
                stickPosition = new Vector3(-_maxRadiusStick, 0);
                _angle = 180;
                OnActive?.Invoke(_angle, Time.deltaTime);
                IsActive?.Invoke(true);
            }

            if (Input.GetKey(KeyCode.D))
            {
                stickPosition = new Vector3(_maxRadiusStick, 0);
                _angle = 0;
                OnActive?.Invoke(_angle, Time.deltaTime);
                IsActive?.Invoke(true);
            }

            if (!Input.anyKey)
            {
                IsActive?.Invoke(false);
            }
        }

        _stick.localPosition = Vector2.ClampMagnitude(stickPosition, _maxRadiusStick);
    }

    private void GetStickPositionAndAngle(Vector3 touchPosition, out Vector3 stickPosition, out float angle)
    {
        touchPosition -= 0.5f * _screenSize;
        stickPosition = touchPosition - _joystickPosition + new Vector3(_maxRadiusStick / 2, _maxRadiusStick / 2);
        angle = Mathf.Atan2(stickPosition.y, stickPosition.x) * Mathf.Rad2Deg;
    }
}