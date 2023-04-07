using System;
using UnityEngine;

public abstract class Ability : MonoBehaviour, IAbility
{
    public event Action<IAbility, IEnemy> OnHit;

    [SerializeField] private AbilityInfo _abilityInfo;

    public AbilityInfo Info { get { return _abilityInfo; } }

    private float _dotDamageTimer = 0;
    private float _dotLifeTimer = 0;
    private float _projectileTimer = 0;

    private void Awake()
    {
        OnCreate();
    }

    private void Update()
    {
        if (_abilityInfo.AbilityDamageType == AbilityInfo.DamageType.DamageOverTime)
        {
            _dotLifeTimer += Time.deltaTime;
            if (_dotLifeTimer >= _abilityInfo.DotDuration)
                Destroy();
        }

        if (_abilityInfo.AbilityType == AbilityInfo.Type.Projectile)
        {
            float deltaX = _abilityInfo.ProjectileSpeed * Time.deltaTime * Mathf.Cos(0);
            float deltaY = _abilityInfo.ProjectileSpeed * Time.deltaTime * Mathf.Sin(0);
            Vector3 deltaPosition = new Vector3(deltaX, deltaY);
            transform.position += deltaPosition;

            _projectileTimer += Time.deltaTime;
            if (_projectileTimer >= _abilityInfo.ProjectileRange)
                Destroy();
        }

        OnUpdate();
    }

    protected abstract void OnCreate();
    protected abstract void OnUpdate();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_abilityInfo.AbilityDamageType == AbilityInfo.DamageType.Hit)
            CallbackOnHit(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_abilityInfo.AbilityDamageType == AbilityInfo.DamageType.DamageOverTime)
        {
            _dotDamageTimer += Time.deltaTime;
            while (_dotDamageTimer >= _abilityInfo.DotRate)
            {
                _dotDamageTimer -= _abilityInfo.DotRate;
                CallbackOnHit(collision);
            }
        }
    }

    private void CallbackOnHit(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyPresenter>())
        {
            EnemyPresenter enemyPresenter = collision.gameObject.GetComponent<EnemyPresenter>();
            IEnemy enemy = enemyPresenter.Enemy;
            OnHit?.Invoke(this, enemy);

            if (_abilityInfo.HaveContinueAbility)
            {
                GameObject continueAbility = Instantiate(_abilityInfo.ContinueAbility.gameObject);
                continueAbility.transform.position = enemyPresenter.transform.position;
            }
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}