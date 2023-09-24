using System;
using UnityEngine;

[System.Serializable]
public class Follower
{
    public event Action<float> OnMove;
    public event Action<bool> IsMoving;

    public float Movespeed;

    private Transform _target;
    private float _distanceMin;

    private Rigidbody2D _rigidbody;
    private ILogic _logic;

    public Follower(ILogic logic, Rigidbody2D rigidbody, float distanceMin)
    {
        _target = logic.Character.Transform;
        _rigidbody = rigidbody;
        Movespeed = 0;
        _distanceMin = distanceMin;
        _logic = logic;
        Enable();
    }

    public void Enable()
    {
        _logic.OnPauseGame += () => FreezeRigidbody(true);
        _logic.OnContinueGame += () => FreezeRigidbody(false);
    }

    public void Disable()
    {
        _logic.OnPauseGame -= () => FreezeRigidbody(true);
        _logic.OnContinueGame -= () => FreezeRigidbody(false);
    }

    public void FixedUpdate(float deltaTime)
    {
        bool isMoving = Vector2.Distance(_rigidbody.position, _target.position) > _distanceMin;
        FreezeRigidbody(!isMoving);
        if (isMoving)
        {
            Vector2 position = _rigidbody.position;
            Vector2 targetPosition = _target.position;
            Vector2 direction = (targetPosition - position).normalized;

            _rigidbody.MovePosition(_rigidbody.position + Movespeed * direction * deltaTime);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            OnMove?.Invoke(angle);
        }
        IsMoving?.Invoke(isMoving);     
    }

    private void FreezeRigidbody(bool freeze)
    {
        if (_rigidbody != null)
        {
            if (freeze)
            {
                _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
                IsMoving?.Invoke(false);
            }
            else
            {
                _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
      
    }
}