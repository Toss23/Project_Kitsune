using System;
using UnityEngine;

public class Joystick : Clickable2D
{
    public event Action<float, float> OnActive;
    public event Action<bool> IsActive;

    [Header("Joystick")]
    [SerializeField] private GameObject _field;
    [SerializeField] private Transform _stick;
    [SerializeField] private int _maxRadiusStick = 200;

    private Vector2 _stickPosition;
    private float _angle;

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

            OnActive?.Invoke(_angle, Time.deltaTime);
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