using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability Info", menuName = "Ability Info")]
public class AbilityInfo : ScriptableObject
{
    public enum DamageType
    {
        Hit, DamageOverTime
    }

    public enum AbilityType
    {
        Melee, Projectile, Field
    }

    [SerializeField] private string _name;
    [SerializeField] private string _description;

    [SerializeField] private bool _useCharacterDamage;
    [SerializeField] private bool _useCharacterCrit;

    [SerializeField] private float[] _duration;
    [SerializeField] private float[] _damage;
    [SerializeField] private float[] _damageMultiplier;
    [SerializeField] private float[] _castPerSecond;
    [SerializeField] private float[] _critChance;
    [SerializeField] private float[] _critMultiplier;
    [SerializeField] private float[] _scale;

    [SerializeField] private bool _haveAura;
    [SerializeField] private GameObject _auraObject;
    
    [SerializeField] private bool _destroyOnHit;

    [SerializeField] private AbilityType _type;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _projectileSpawnOffset;
    [SerializeField] private int[] _projectileCount;
    [SerializeField] private float[] _projectileAngle;
    [SerializeField] private bool _projectileFollowsTarget;
    [SerializeField] private bool _projectileAimTarget;

    [SerializeField] private DamageType _abilityDamageType;
    [SerializeField] private float[] _dotRate;

    [SerializeField] private AbilityProperty[] _abilityProperties;

    public bool UseCharacterDamage => _useCharacterDamage;
    public bool UseCharacterCrit => _useCharacterCrit;

    public DamageType AbilityDamageType => _abilityDamageType;
    public float[] DotRate => _dotRate;

    public AbilityType Type => _type;

    public float ProjectileSpeed => _projectileSpeed;
    public float ProjectileSpawnOffset => _projectileSpawnOffset;
    public int[] ProjectileCount => _projectileCount;
    public float[] ProjectileAngle => _projectileAngle;
    public bool ProjectileFollowsTarget => _projectileFollowsTarget;
    public bool ProjectileAimTarget => _projectileAimTarget;
    public bool DestroyOnHit => _destroyOnHit;

    public bool HaveAura => _haveAura;
    public GameObject AuraObject => _auraObject;

    public float[] Duration => _duration;
    public float[] Damage => _damage;
    public float[] DamageMultiplier => _damageMultiplier;
    public float[] CastPerSecond => _castPerSecond;
    public float[] CritChance => _critChance;
    public float[] CritMultiplier => _critMultiplier;

    public string Name => _name;
    public string Description => _description;

    public float[] Scale => _scale;

    public AbilityProperty[] AbilityProperties => _abilityProperties;
}