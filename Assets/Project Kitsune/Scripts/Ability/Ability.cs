using System;
using UnityEngine;

public abstract class Ability : MonoBehaviour, IAbility
{
    public event Action<Ability, IEnemy> OnHit;

    [SerializeField] protected bool _useCharacterDamage = true;
    [SerializeField] protected bool _useCharacterCrit = false;

    [SerializeField] protected IAbility.DamageType _abilityDamageType = IAbility.DamageType.Hit;
    [SerializeField] protected IAbility.Type _abilityType = IAbility.Type.Melee;
    [SerializeField] protected float _projectileSpeed;

    [SerializeField] protected float[] _damage;
    [SerializeField] protected float[] _damageMultiplier;

    [SerializeField] protected float[] _castPerSecond;

    [SerializeField] protected float[] _critChance;
    [SerializeField] protected float[] _critMultiplier;

    [SerializeField] protected string _description;

    public int Level { get; private set; }
    public bool UseCharacterDamage { get { return _useCharacterDamage; } }
    public bool UseCharacterCrit { get { return _useCharacterCrit; } }
    public IAbility.DamageType AbilityDamageType { get { return _abilityDamageType; } }
    public IAbility.Type AbilityType { get { return _abilityType; } }
    public float[] Damage { get { return _damage; } }
    public float[] DamageMultiplier { get { return _damageMultiplier; } }
    public float[] CastPerSecond { get { return _castPerSecond; } }
    public float[] CritChance { get { return _critChance; } }
    public float[] CritMultiplier { get { return _critMultiplier; } }

    public string Description { get { return _description; } }

    private float _dotTimer = 0;

    private void Awake()
    {
        OnCreate();
    }

    private void Update()
    {
        OnUpdate();
    }

    protected abstract void OnCreate();
    protected abstract void OnUpdate();

    public void SetLevel(int level)
    {
        Level = level;
        if (Level > Damage.Length)
            Level = Damage.Length;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_abilityDamageType == IAbility.DamageType.Hit)
            CallbackOnHit(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_abilityDamageType == IAbility.DamageType.DamageOverTime)
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
}