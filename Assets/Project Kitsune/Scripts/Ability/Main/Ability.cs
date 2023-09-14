using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Ability : MonoBehaviour, IAbility
{
    public event Action<IAbility, Unit> OnHit;

    [Header("Data")]
    [SerializeField] private AbilityData _abilityData;

    // Init
    protected int _abilityIndex;
    protected int _level;
    protected Unit _caster;
    protected UnitType _target;
    protected AbilityModifier _abilityModifier;
    protected Dictionary<string, float> _properties;

    // Range
    protected RangeAbilityData _rangeAbilityData;
    protected Transform _nearestEnemy;

    // Private fields
    protected IGameLogic _gameLogic;

    // Logic
    private float _duration;

    // References
    public AbilityData AbilityData => _abilityData;
    public int Level => _level;
    public AbilityModifier AbilityModifier => _abilityModifier;
    public Dictionary<string, float> Properties => _properties;

    public void Init(int abilityIndex, int level, Unit caster, UnitType target, AbilityModifier abilityModifier)
    {
        // Game Logic
        _gameLogic = GameLogic.Instance;
        _gameLogic.OnUpdate += UpdateAbility;

        // References
        _abilityIndex = abilityIndex;
        _level = level;
        _caster = caster;
        _target = target;
        _abilityModifier = abilityModifier;

        // Properties
        _properties = AbilityProperty.ListToDictionary(_level, _abilityData.AbilityProperties);

        if (_abilityModifier.Properties != null)
        {
            foreach (KeyValuePair<string, float> pair in _abilityModifier.Properties)
            {
                if (_properties.ContainsKey(pair.Key))
                {
                    _properties[pair.Key] += pair.Value;
                }
            }
        }

        // Rescale
        transform.localScale *= _abilityData.Scale.Get(_level) + abilityModifier.Scale;

        // Spawn on nearest enemy
        if (_abilityData.SpawnOnNearestEnemy)
        {
            _nearestEnemy = FindNearestEnemy();
            if (_nearestEnemy != null)
            {
                if (_abilityData.SpawnRange == 0)
                {
                    transform.position = _nearestEnemy.position;
                }
                else
                {
                    if (Vector2.Distance(transform.position, _nearestEnemy.position) <= _abilityData.SpawnRange)
                    {
                        transform.position = _nearestEnemy.position;
                    }
                    else
                    {
                        DestroyAbility();
                    }
                }
            }
        }

        // Range Ability
        if (_abilityData.GetType() == typeof(RangeAbilityData))
        {
            InitRangeAbility();
        }

        OnCreateAbility();
    }

    private void InitRangeAbility()
    {
        // Get Range Ability Data
        _rangeAbilityData = (RangeAbilityData)_abilityData;

        // Find nearest enemy
        if (_rangeAbilityData.AimNearestEnemy | _rangeAbilityData.FollowNearestEnemy)
        {
            _nearestEnemy = FindNearestEnemy();

            // Rotate ability if need aim
            if (_nearestEnemy != null)
            {
                if (_rangeAbilityData.AimNearestEnemy)
                {
                    Vector2 deltaPosition = _nearestEnemy.transform.position - transform.position;
                    float targetAngle = Mathf.Atan2(deltaPosition.y, deltaPosition.x);
                    Quaternion quaternion = Quaternion.Euler(0, 0, targetAngle * Mathf.Rad2Deg);
                    transform.rotation = quaternion;
                }
            }
        }

        // Add offset to position
        if (_rangeAbilityData.SpawnOffset != 0)
        {
            float angle = transform.eulerAngles.z;
            Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
            transform.position += offset * _rangeAbilityData.SpawnOffset;
        }
    }

    private Transform FindNearestEnemy()
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag(_target == UnitType.Enemy ? "Enemy" : "Character");
        if (units.Length > 0)
        {
            float distanceMin = 100000;
            GameObject nearestEnemy = null;
            foreach (GameObject unit in units)
            {
                float distance = Vector3.Distance(transform.position, unit.transform.position);
                if (distance < distanceMin)
                {
                    distanceMin = distance;
                    nearestEnemy = unit;
                }
            }
            return nearestEnemy.transform;
        }
        return null;
    }

    protected virtual void OnCreateAbility() { }

    private void UpdateAbility(float deltaTime)
    {
        OnUpdateAbility(deltaTime);

        // Update Duration
        if (_abilityData.HaveDuration)
        {
            _duration += deltaTime;
            if (_duration >= _abilityData.Duration.Get(_level))
            {
                DestroyAbility();
            }
        }

        // Fuse position with caster
        if (_abilityData.FuseWithCaster)
        {
            if (_caster != null)
            {
                GameObject point = _caster.UnitPresenter.UnitView.AbilityPoints.Points[_abilityIndex];
                if (point != null)
                {
                    transform.position = point.transform.position;
                }
                else
                {
                    DestroyAbility();
                }
            }
        }
        else
        {
            // Update Range Ability
            if (_rangeAbilityData != null)
            {
                UpdateRangeAbility(deltaTime);
            }
        }
        
        OnLateUpdateAbility(deltaTime);
    }

    private void UpdateRangeAbility(float deltaTime)
    {
        // Move position
        float deltaX = Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad);
        float deltaY = Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad);
        Vector3 deltaPosition = new Vector3(deltaX, deltaY) * _rangeAbilityData.Speed * deltaTime;

        // Follow enemy
        if (_nearestEnemy != null)
        {
            if (_rangeAbilityData.FollowNearestEnemy)
            {
                Vector3 deltaPositionWithEnemy = _nearestEnemy.transform.position - transform.position;
                float targetAngle = Mathf.Atan2(deltaPositionWithEnemy.y, deltaPositionWithEnemy.x);
                Quaternion quaternion = Quaternion.Euler(0, 0, targetAngle * Mathf.Rad2Deg);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternion, 360 * deltaTime);

                if (deltaPosition.magnitude > deltaPositionWithEnemy.magnitude)
                {
                    deltaPosition = deltaPositionWithEnemy;
                }
            }
        }

        // Move position
        transform.position += deltaPosition;
    }

    protected virtual void OnUpdateAbility(float deltaTime) { }
    protected virtual void OnLateUpdateAbility(float deltaTime) { }

    public void DestroyAbility()
    {
        OnDestroyAbility();
        _gameLogic.OnUpdate -= UpdateAbility;
        Destroy(gameObject);
    }

    protected virtual void OnDestroyAbility() { }

    protected Unit HitCollisionEnemy(Collider2D collision)
    {
        IUnitPresenter unitPresenter = collision.gameObject.GetComponent<IUnitPresenter>();
        if (unitPresenter != null)
        {
            if (collision.transform.tag == _target.ToString())
            {
                Unit unit = unitPresenter.Unit;
                OnHit?.Invoke(this, unit);
                return unit;
            }
        }
        return null;
    }
}
