using UnityEngine;

public class OnniAbility1 : Ability
{
    private float _maxRadius;
    private float _currentRadius;
    private float _speed;
    private bool _needScaleDown;

    protected override void OnCollisionEnterWithEnemy(IUnit enemy)
    {
        
    }

    protected override void OnCollisionStayWithEnemy(IUnit enemy)
    {
        
    }

    protected override void OnCreate()
    {
        _needScaleDown = false;
        _currentRadius = 1;
        _maxRadius = 8;
        _speed = 2;
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (_needScaleDown == false)
        {
            _currentRadius += Time.deltaTime * _speed;

            if (_currentRadius >= _maxRadius / 2f)
                _needScaleDown = true;
        }
        else
        {
            _currentRadius -= Time.deltaTime * _speed;

            if (_currentRadius <= 1)
            {
                Destroy();
            }
        }

        transform.localScale = new Vector3(_currentRadius, _currentRadius) * Info.Radius[Level];
    }
}