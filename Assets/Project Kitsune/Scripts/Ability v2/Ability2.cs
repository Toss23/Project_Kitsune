using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability2 : MonoBehaviour, IAbility2
{
    public event Action<IAbility2, Unit> OnHit;

    [SerializeField] private AbilityData _abilityData;

    // Init
    protected int _level;
    protected Unit _caster;
    protected UnitType _target;
    protected AbilityModifier _abilityModifier;
    protected Dictionary<string, float> _properties;

    // Range
    protected RangeAbilityData _rangeAbilityData;
    protected Transform _nearestEnemy;

    // Private fields
    private IGameLogic _gameLogic;

    // Logic
    private float _duration;

    // References
    public AbilityData AbilityData => _abilityData;

    public void Init(int level, Unit caster, UnitType target, AbilityModifier abilityModifier)
    {
        // References
        _level = level;
        _caster = caster;
        _target = target;
        _abilityModifier = abilityModifier;

        // Properties
        _properties = AbilityProperty.ListToDictionary(_level, _abilityData.AbilityProperties);

        // Rescale
        transform.localScale *= _abilityData.Scale[_level] + abilityModifier.Radius;

        // Range Ability
        if (_abilityData.GetType() == typeof(RangeAbilityData))
        {
            InitRangeAbility();
        }

        _gameLogic = GameLogic.Instance;
        _gameLogic.OnUpdate += UpdateAbility;

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

    protected abstract void OnCreateAbility();

    private void UpdateAbility(float deltaTime)
    {
        OnUpdate(deltaTime);

        // Update Duration
        if (_abilityData.HaveDuration)
        {
            _duration += deltaTime;
            if (_duration >= _abilityData.Duration[_level])
            {
                DestroyAbility();
            }
        }

        // Fuse position with caster
        if (_abilityData.FuseWithCaster)
        {
            transform.position = _caster.UnitPresenter.Transform.position;
        }
        else
        {
            // Update Range Ability
            if (_rangeAbilityData != null)
            {
                UpdateRangeAbility(deltaTime);
            }
        }
        
        OnLateUpdate(deltaTime);
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

    protected abstract void OnUpdate(float deltaTime);
    protected abstract void OnLateUpdate(float deltaTime);

    public void DestroyAbility()
    {
        OnDestroyAbility();
        _gameLogic.OnUpdate -= UpdateAbility;
        Destroy(gameObject);
    }

    protected abstract void OnDestroyAbility();

    protected Unit HitCollisionEnemy(Collision2D collision)
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
