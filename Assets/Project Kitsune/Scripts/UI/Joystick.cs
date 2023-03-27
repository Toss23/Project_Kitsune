using System;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    public event Action<float, float> OnActive;

    [SerializeField] private Transform _stick;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private int _maxRadiusStick = 200;

    private Vector3 _screenSize;
    private Vector2 _joystickPosition;
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
        Vector2 stickPosition = Vector2.zero;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 mousePosition = Input.mousePosition - 0.5f * _screenSize;
            stickPosition = mousePosition - _joystickPosition;
            _angle = Mathf.Atan2(stickPosition.y, stickPosition.x) * Mathf.Rad2Deg;

            OnActive?.Invoke(_angle, Time.deltaTime);
        }

        _stick.localPosition = Vector2.ClampMagnitude(stickPosition, _maxRadiusStick);
    }
}