using System;
using UnityEngine;

public class Controlable
{
    public event Action<Vector2> OnMove;

    public float Speed;

    private Vector2 _position;
    private bool _freeze = false;

    public void Freeze(bool state)
    {
        _freeze = state;
    }

    public void Move(float angle, float deltaTime)
    {
        if (_freeze == false)
        {
            angle *= Mathf.Deg2Rad;
            float deltaX = Mathf.Cos(angle) * Speed * deltaTime;
            float deltaY = Mathf.Sin(angle) * Speed * deltaTime;
            _position += new Vector2(deltaX, deltaY);
            OnMove?.Invoke(_position);
        }
    }
}