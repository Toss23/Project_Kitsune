using System;
using UnityEngine;

public class Follower
{
    public event Action<float> OnMove;
    public event Action<bool> IsMoving;

    private Transform _target;
    private float _speed;
    private float _distanceMin;

    private Rigidbody2D _rigidbody;

    public Follower(Transform target, Rigidbody2D rigidbody, float speed, float distanceMin)
    {
        _target = target;
        _rigidbody = rigidbody;
        _speed = speed;
        _distanceMin = distanceMin;
    }

    public void FixedUpdate(float deltaTime)
    {
        bool isMoving = Vector2.Distance(_rigidbody.position, _target.position) >= _distanceMin;
        if (isMoving)
        {
            Vector2 position = _rigidbody.position;
            Vector2 targetPosition = _target.position;
            Vector2 direction = (targetPosition - position).normalized;

            _rigidbody.MovePosition(_rigidbody.position + _speed * direction * deltaTime);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            OnMove?.Invoke(angle);
        }
        IsMoving?.Invoke(isMoving);     
    }
}