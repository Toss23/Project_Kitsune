using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Ability : MonoBehaviour, IAbility
{
    public event Action<IAbility, IUnit> OnHit;

    [SerializeField] private AbilityInfo _info;

    public AbilityInfo Info { get { return _info; } }

    private IUnit _caster;
    private UnitType _target;

    private int _level;
    private float _dotDamageTimer = 0;
    private float _dotLifeTimer = 0;
    private float _projectileTimer = 0;
    private float _meleeTimer = 0;
    private Transform _pointToFusing;
    private Dictionary<string, float> _properties;
    private Transform _targetedEnemy;

    private AbilityModifier _abilityModifier;

    public int Level => _level;
    public int MaxLevel => _info.Damage.Length - 1;
    public Dictionary<string, float> Properties => _properties;
    public Transform TargetedEnemy => _targetedEnemy;
    public AbilityModifier AbilityModifier => _abilityModifier;

    public void Init(int level, IUnit caster, UnitType target, AbilityModifier abilityModifier)
    {
        _caster = caster;
        _target = target;
        _abilityModifier = abilityModifier;

        _level = level;
        if (_level > MaxLevel)
            _level = MaxLevel;

        transform.localScale *= _info.Radius[_level] + abilityModifier.Radius;

        if (_info.AbilityType == AbilityInfo.Type.Projectile)
        {
            _targetedEnemy = FindNearestEnemy();

            if (_info.ProjectileAutoAim)
            {
                if (_targetedEnemy != null)
                {
                    Vector2 delta = _targetedEnemy.transform.position - transform.position;
                    float targetAngle = Mathf.Atan2(delta.y, delta.x);
                    Quaternion quaternion = Quaternion.Euler(0, 0, targetAngle * Mathf.Rad2Deg);
                    transform.rotation = quaternion;
                }
            }

            if (_info.ProjectileSpawnOffset > 0)
            {
                float angle = transform.eulerAngles.z;
                Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
                transform.position += offset * _info.ProjectileSpawnOffset;
            }
        }

        _properties = new Dictionary<string, float>();
        foreach (AbilityProperty param in _info.AbilityProperties)
        {
            _properties.Add(param.Name, param.Values[_level]);
        }

        OnCreateAbility(_caster);
        GameLogic.Instance.OnUpdate += UpdateAbility;
    }

    protected Transform FindNearestEnemy()
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

    public void FuseWith(Transform transform)
    {
        _pointToFusing = transform;
    }

    private void UpdateAbility(float deltaTime)
    {
        if (_info.AbilityType == AbilityInfo.Type.Melee & _info.AbilityDamageType != AbilityInfo.DamageType.DamageOverTime)
        {
            _meleeTimer += deltaTime;
            if (_meleeTimer >= _info.MeleeAnimationTime)
                Destroy();
        }

        if (_info.AbilityDamageType == AbilityInfo.DamageType.DamageOverTime && _info.AbilityType != AbilityInfo.Type.Field)
        {
            _dotLifeTimer += deltaTime;
            if (_dotLifeTimer >= _info.DotDuration[_level] + _abilityModifier.DotDuration)
                Destroy();
        }

        if (_info.AbilityType == AbilityInfo.Type.Projectile)
        {
            if (_info.ProjectileAutoTarget)
            {
                if (_targetedEnemy != null)
                {
                    Vector2 delta = _targetedEnemy.transform.position - transform.position;
                    float targetAngle = Mathf.Atan2(delta.y, delta.x);
                    Quaternion quaternion = Quaternion.Euler(0, 0, targetAngle * Mathf.Rad2Deg);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternion, 360 * deltaTime);
                }
            }

            float deltaX = Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad);
            float deltaY = Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad);
            Vector3 deltaPosition = new Vector3(deltaX, deltaY) * _info.ProjectileSpeed * deltaTime;
            transform.position += deltaPosition;

            _projectileTimer += deltaTime;
            if (_projectileTimer >= _info.ProjectileRange)
                Destroy();
        }

        if (_info.AbilityType == AbilityInfo.Type.Field)
        {
            if (_pointToFusing != null)
                transform.position = _pointToFusing.transform.position;
        }

        OnUpdateAbility(_caster, deltaTime);
    }

    protected virtual void OnCollisionWithObject(GameObject gameObject) { }
    protected abstract void OnCollisionStayWithEnemy(IUnit caster, IUnit target);
    protected abstract void OnCollisionEnterWithEnemy(IUnit caster, IUnit target);
    protected abstract void OnDestroyAbility(IUnit caster);
    protected abstract void OnUpdateAbility(IUnit caster, float deltaTime);
    protected abstract void OnCreateAbility(IUnit caster);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_info.AbilityDamageType == AbilityInfo.DamageType.Hit)
        {
            IUnit target = CallbackOnHit(collision);
            if (target != null)
            {
                OnCollisionEnterWithEnemy(_caster, target);
                
                if (_info.DestroyOnHit)
                {
                    Destroy();
                }
            }
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (GameLogic.Instance.Paused == false)
        {
            if (_info.AbilityDamageType == AbilityInfo.DamageType.DamageOverTime)
            {
                _dotDamageTimer += Time.deltaTime;

                float rate = Mathf.Max(_info.DotRate[_level] + _abilityModifier.DotRate, 0.1f);

                while (_dotDamageTimer >= rate)
                {
                    _dotDamageTimer -= rate;
                    IUnit target = CallbackOnHit(collision);
                    if (target != null)
                    {
                        OnCollisionStayWithEnemy(_caster, target);
                        
                        if (_info.DestroyOnHit)
                        {
                            Destroy();
                        }
                    }
                }
            }

            if (collision.tag != "Untagged")
                OnCollisionWithObject(collision.gameObject);
        }
    }

    private IUnit CallbackOnHit(Collider2D collision)
    {
        IUnitPresenter unitPresenter = collision.gameObject.GetComponent<IUnitPresenter>();
        if (unitPresenter != null)
        {
            if (collision.transform.tag == _target.ToString())
            {
                IUnit unit = unitPresenter.Unit;
                OnHit?.Invoke(this, unit);

                if (_info.HaveContinueAbility)
                {
                    GameObject continueAbility = Instantiate(_info.ContinueAbility.gameObject);
                    continueAbility.transform.position = unitPresenter.Transform.position;
                }

                return unit;
            }
        }
        return null;
    }

    public void Destroy()
    {
        OnDestroyAbility(_caster);
        GameLogic.Instance.OnUpdate -= UpdateAbility;
        Destroy(gameObject);
    }
}