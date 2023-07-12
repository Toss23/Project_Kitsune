using UnityEngine;

public class Controlable
{
    private float _speed;
    private Rigidbody2D _rigidbody;
    private bool _active;
    private Vector2 _position;

    public Controlable(Rigidbody2D rigidbody, float speed)
    {
        _rigidbody = rigidbody;
        _speed = speed;
        _position = rigidbody.position;
        _active = true;
    }

    public void SetActive(bool active)
    {
        _active = active;

        if (active)
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        else
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void Move(float angle, float deltaTime)
    {
        if (_active)
        {
            float deltaX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float deltaY = Mathf.Sin(angle * Mathf.Deg2Rad);
            _position += new Vector2(deltaX, deltaY) * _speed * deltaTime;
        }
    }

    public void FixedUpdate(float deltaTime)
    {
        _rigidbody.MovePosition(_position);
    }
}