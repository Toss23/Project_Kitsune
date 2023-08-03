using UnityEngine;

public class FireSpheres : Ability
{
    [SerializeField] private GameObject _circlePrefab;
    [SerializeField] private int _count = 5;
    [SerializeField] private float _speed = 90;
    [SerializeField] private float _radius = 3;

    private GameObject[] _circles;
    private float _angle;

    protected override void OnCollisionEnterWithEnemy(IUnit caster, IUnit target)
    {
        
    }

    protected override void OnCollisionStayWithEnemy(IUnit caster, IUnit target)
    {
        
    }

    protected override void OnCreateAbility(IUnit caster)
    {
        _angle = 0;
        _circles = new GameObject[_count]; 
        for (int i = 0; i < _count; i++)
        {
            _circles[i] = Instantiate(_circlePrefab, transform);
            _circles[i].transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    protected override void OnUpdateAbility(IUnit caster, float deltaTime)
    {
        _angle -= deltaTime * _speed;

        if (_angle <= -360)
        {
            _angle += 360;
        }

        float deltaAngle = 360f / _count;

        for (int i = 0; i < _count; i++)
        {
            Vector2 position = new Vector2();
            position.x = _radius * Mathf.Cos((_angle + deltaAngle * i) * Mathf.Deg2Rad);
            position.y = _radius * Mathf.Sin((_angle + deltaAngle * i) * Mathf.Deg2Rad);
            _circles[i].transform.localPosition = position;
        }
    }

    protected override void OnDestroyAbility(IUnit caster)
    {

    }
}