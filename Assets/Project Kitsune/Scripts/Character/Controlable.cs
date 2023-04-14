using UnityEngine;

public class Controlable
{
    private float _speed;
    private Rigidbody2D _rigidbody;
    private bool _freeze = false;
    private Vector2 _position;

    public Controlable(Rigidbody2D rigidbody, float speed)
    {
        _rigidbody = rigidbody;
        _speed = speed;
        _position = rigidbody.position;
    }

    public void Freeze(bool state)
    {
        _freeze = state;

        if (state)
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        else
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void Move(float angle, float deltaTime)
    {
        if (_freeze == false)
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