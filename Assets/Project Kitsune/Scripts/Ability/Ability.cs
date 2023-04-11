using System;
using UnityEngine;

public enum AbilityType
{
    Attack, First, Second, Third, Ultimate
}

public abstract class Ability : MonoBehaviour, IAbility
{
    public event Action<IAbility, IEnemy> OnHit;

    [SerializeField] private AbilityInfo _info;

    public AbilityInfo Info { get { return _info; } }

    private int _level;
    private float _dotDamageTimer = 0;
    private float _dotLifeTimer = 0;
    private float _projectileTimer = 0;
    private float _meleeTimer = 0;
    private Transform _pointToFusing;
    private bool _fused = false;

    private Transform _nearestEnemy;

    public int Level => _level;
    public int MaxLevel => _info.Damage.Length - 1;

    public void Init(int level)
    {
        _level = level;
        if (_level > MaxLevel)
            _level = MaxLevel;

        transform.localScale *= _info.Radius[_level];
    }

    public void FuseWith(Transform transform)
    {
        _pointToFusing = transform;
    }

    private void Awake()
    {   
        if (_info.AbilityType == AbilityInfo.Type.Projectile & _info.ProjectileAuto)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length > 0)
            {
                float distanceMin = 100000;
                GameObject nearestEnemy = null;
                foreach (GameObject enemy in enemies)
                {
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distance < distanceMin)
                    {
                        distanceMin = distance;
                        nearestEnemy = enemy;
                    }
                }
                _nearestEnemy = nearestEnemy.transform;
            }
        }

        OnCreate();
    }

    private void Update()
    {
        if (_info.AbilityType == AbilityInfo.Type.Melee)
        {
            _meleeTimer += Time.deltaTime;
            if (_meleeTimer >= _info.MeleeAnimationTime)
                Destroy();
        }

        if (_info.AbilityDamageType == AbilityInfo.DamageType.DamageOverTime && _info.AbilityType != AbilityInfo.Type.Field)
        {
            _dotLifeTimer += Time.deltaTime;
            if (_dotLifeTimer >= _info.DotDuration[_level])
                Destroy();
        }

        if (_info.AbilityType == AbilityInfo.Type.Projectile)
        {
            if (_info.ProjectileAuto)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, _nearestEnemy.rotation, 360 * Time.deltaTime);
            }

            float deltaX = Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad);
            float deltaY = Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad);
            Vector3 deltaPosition = new Vector3(deltaX, deltaY) * _info.ProjectileSpeed * Time.deltaTime;
            transform.position += deltaPosition;

            _projectileTimer += Time.deltaTime;
            if (_projectileTimer >= _info.ProjectileRange)
                Destroy();
        }

        if (_info.AbilityType == AbilityInfo.Type.Field)
        {
            if (_pointToFusing != null & _fused == false)
            {
                transform.parent = _pointToFusing;
                _fused = true;
            }
        }

        OnUpdate();
    }

    protected abstract void OnCreate();
    protected abstract void OnUpdate();
    protected abstract void OnCollisionStayWithEnemy(IEnemy enemy);
    protected abstract void OnCollisionEnterWithEnemy(IEnemy enemy);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_info.AbilityDamageType == AbilityInfo.DamageType.Hit)
        {
            IEnemy enemy = CallbackOnHit(collision);
            OnCollisionEnterWithEnemy(enemy);
            DestroyOnHit();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_info.AbilityDamageType == AbilityInfo.DamageType.DamageOverTime)
        {
            _dotDamageTimer += Time.deltaTime;
            while (_dotDamageTimer >= _info.DotRate[_level])
            {
                _dotDamageTimer -= _info.DotRate[_level];
                IEnemy enemy = CallbackOnHit(collision);
                OnCollisionStayWithEnemy(enemy);
                DestroyOnHit();
            }
        }
    }

    private IEnemy CallbackOnHit(Collision2D collision)
    {
        EnemyPresenter enemyPresenter = collision.gameObject.GetComponent<EnemyPresenter>();
        if (enemyPresenter)
        {
            IEnemy enemy = enemyPresenter.Enemy;
            OnHit?.Invoke(this, enemy);

            if (_info.HaveContinueAbility)
            {
                GameObject continueAbility = Instantiate(_info.ContinueAbility.gameObject);
                continueAbility.transform.position = enemyPresenter.transform.position;
            }

            return enemy;
        }
        return null;
    }

    private void DestroyOnHit()
    {
        if (_info.AbilityType == AbilityInfo.Type.Projectile && _info.DestroyOnHit)
            Destroy();
        else if (_info.AbilityType != AbilityInfo.Type.Projectile)
            Destroy();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}