using System;
using UnityEngine;

public abstract class Ability : MonoBehaviour, IAbility
{
    public event Action<Ability, IEnemy> OnHit;

    [Header("Base")]
    [SerializeField] protected bool _useCharacterDamage = true;
    [SerializeField] protected bool _useCharacterCrit = false;

    [Header("Damage")]
    [SerializeField] protected float _damage = 0;
    [SerializeField] protected float _damageMultiplier = 0;

    [Header("Crit")]
    [SerializeField] protected float _critChance = 0;
    [SerializeField] protected float _critMultiplier = 0;

    public bool UseCharacterDamage { get { return _useCharacterDamage; } }
    public bool UseCharacterCrit { get { return _useCharacterCrit; } }
    public float Damage { get { return _damage; } }
    public float DamageMultiplier { get { return _damageMultiplier; } }
    public float CritChance { get { return _critChance; } }
    public float CritMultiplier { get { return _critMultiplier; } }

    private void Awake()
    {
        Use();
    }

    protected abstract void Use();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyPresenter>())
        {
            EnemyPresenter enemyPresenter = collision.gameObject.GetComponent<EnemyPresenter>();
            IEnemy enemy = enemyPresenter.Enemy;
            OnHit?.Invoke(this, enemy);
        }
    }
}