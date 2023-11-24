using System;
using UnityEngine;

public class Joystick : Clickable2D
{
    // active, angle
    public event Action<bool, float> OnMoveStick;
    public event Action OnActiveChanged;

    [Header("Joystick")]
    [SerializeField] private GameObject _field;
    [SerializeField] private Transform _stick;
    [SerializeField] private int _maxRadiusStick = 200;

    private Vector2 _stickPosition;
    private float _angle;
    private bool _active = false;

    private void Awake()
    {
        _field.SetActive(false);
        StopMove();
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            _stickPosition = Vector3.zero;

            if (Touched)
            {
                _stickPosition = TouchPositionDelta;
                _angle = Mathf.Atan2(_stickPosition.y, _stickPosition.x) * Mathf.Rad2Deg;
                _stick.localPosition = Vector2.ClampMagnitude(_stickPosition, _maxRadiusStick);

                if (_active == false)
                {
                    if (TouchPositionDelta.magnitude >= 80)
                    {
                        Moving();
                    }
                }
                else
                {
                    Moving();
                }
            }
            else
            {
                StopMove();
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            _angle = 0;

            if (Input.GetKey(KeyCode.W))
            {
                _angle = 90;
            }
            
            if (Input.GetKey(KeyCode.S))
            {
                _angle = -90;
            }

            if (Input.GetKey(KeyCode.A))
            {
                _angle = 180;

                if (Input.GetKey(KeyCode.W))
                {
                    _angle = 135;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    _angle = -135;
                }
            }
            
            if (Input.GetKey(KeyCode.D))
            {
                _angle = 0;

                if (Input.GetKey(KeyCode.W))
                {
                    _angle = 45;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    _angle = -45;
                }
            }

            if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.D))
            {
                Moving();
                OnMoveStick?.Invoke(true, _angle);
            }
            else
            {
                StopMove();
            }
        }
    }

    private void Moving()
    {
        OnMoveStick?.Invoke(true, _angle);

        if (_active == false)
        {
            OnActiveChanged?.Invoke();
        }

        _active = true;
    }

    private void StopMove()
    {
        OnMoveStick?.Invoke(false, 0);

        if (_active == true)
        {
            OnActiveChanged?.Invoke();
        }

        _active = false;
    }

    protected override void OnTouchDown()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            _field.SetActive(true);
            _field.transform.localPosition = TouchPosition;
        }
    }

    protected override void OnTouchUp()
    {
        _field.SetActive(false);
    }

    protected override void OnSingleClick() { }
}