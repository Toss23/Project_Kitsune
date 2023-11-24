using UnityEngine;

[System.Serializable]
public class Controllable
{
    public float Movespeed;
    private Rigidbody2D _rigidbody;
    private bool _active;

    public Controllable(Rigidbody2D rigidbody)
    {
        _rigidbody = rigidbody;
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

    private bool _moving;
    private float _angle;

    public void Move(bool moving, float angle)
    {
        _moving = moving;
        _angle = angle;
    }

    public void FixedUpdate(float deltaTime)
    {
        if (_moving == true & _active == true)
        {
            float deltaX = Mathf.Cos(_angle * Mathf.Deg2Rad);
            float deltaY = Mathf.Sin(_angle * Mathf.Deg2Rad);
            _rigidbody.MovePosition(_rigidbody.transform.position + new Vector3(deltaX, deltaY) * Movespeed * deltaTime);
        }
    }
}