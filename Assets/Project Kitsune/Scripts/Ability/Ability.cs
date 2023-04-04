using System;
using UnityEngine;

public abstract class Ability : MonoBehaviour, IAbility
{
    public event Action<IAbility, IEnemy> OnHit;

    [SerializeField] private AbilityInfo _abilityInfo;

    public AbilityInfo Info { get { return _abilityInfo; } }

    private float _dotTimer = 0;
    private float _projectileTimer = 0;

    private void Awake()
    {
        OnCreate();
    }

    private void Update()
    {
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
            _dotTimer += Time.deltaTime;
            while (_dotTimer >= 1)
            {
                _dotTimer--;
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
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}