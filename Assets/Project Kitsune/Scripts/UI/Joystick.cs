using System;
using UnityEngine;

public class Joystick : Clickable2D
{
    public event Action<float, float> OnTouched;
    public event Action<bool> IsActive;
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
    }

    protected override void OnUpdate(float deltaTime)
    {
        _stickPosition = Vector3.zero;

        if (Touched)
        {
            _stickPosition = TouchPositionDelta;
            _angle = Mathf.Atan2(_stickPosition.y, _stickPosition.x) * Mathf.Rad2Deg;
            _stick.localPosition = Vector2.ClampMagnitude(_stickPosition, _maxRadiusStick);

            OnTouched?.Invoke(_angle, Time.deltaTime);

            if (_active == false)
            {
                OnActiveChanged?.Invoke();
            }

            _active = true;
        }
        else
        {
            if (_active == true)
            {
                OnActiveChanged?.Invoke();
            }

            _active = false;
        }

        IsActive?.Invoke(Touched);
    }

    protected override void OnTouchDown()
    {
        _field.SetActive(true);
        _field.transform.localPosition = TouchPosition;
    }

    protected override void OnTouchUp()
    {
        _field.SetActive(false);
    }

    protected override void OnSingleClick() { }
}