using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability Info", menuName = "Create Ability Info")]
public class AbilityInfo : ScriptableObject
{
    public enum DamageType
    {
        Hit, DamageOverTime
    }

    public enum Type
    {
        Melee, Projectile, Field
    }

    [SerializeField] private bool _useCharacterDamage = true;
    [SerializeField] private bool _useCharacterCrit = false;

    [SerializeField] private DamageType _abilityDamageType = DamageType.Hit;
    [SerializeField] private float[] _dotRate;
    [SerializeField] private float[] _dotDuration;

    [SerializeField] private Type _abilityType = Type.Projectile;
    [SerializeField] private float _meleeAnimationTime;

    [SerializeField] private float _projectileSpeed = 10;
    [SerializeField] private float _projectileRange = 1;
    [SerializeField] private float _projectileSpawnOffset = 0;
    [SerializeField] private int[] _projectileCount;
    [SerializeField] private float[] _projectileAngle;
    [SerializeField] private bool _projectileAutoTarget;
    [SerializeField] private bool _projectileAutoAim;
    [SerializeField] private bool _destroyOnHit;

    [SerializeField] private bool _haveContinueAbility;
    [SerializeField] private Ability _continueAbility;

    [SerializeField] private bool _haveAura;
    [SerializeField] private GameObject _auraObject;

    [SerializeField] private float[] _damage;
    [SerializeField] private float[] _damageMultiplier;

    [SerializeField] private float[] _castPerSecond;

    [SerializeField] private float[] _critChance;
    [SerializeField] private float[] _critMultiplier;

    [SerializeField] private string _name;
    [SerializeField] private string _description;

    [SerializeField] private float[] _radius;

    [SerializeField] private AbilityProperty[] _abilityProperties;

    public bool UseCharacterDamage => _useCharacterDamage;
    public bool UseCharacterCrit => _useCharacterCrit;

    public DamageType AbilityDamageType => _abilityDamageType;
    public float[] DotRate => _dotRate;
    public float[] DotDuration => _dotDuration;

    public Type AbilityType => _abilityType;
    public float MeleeAnimationTime => _meleeAnimationTime;

    public float ProjectileSpeed => _projectileSpeed;
    public float ProjectileRange => _projectileRange;
    public float ProjectileSpawnOffset => _projectileSpawnOffset;
    public int[] ProjectileCount => _projectileCount;
    public float[] ProjectileAngle => _projectileAngle;
    public bool ProjectileAutoTarget => _projectileAutoTarget;
    public bool ProjectileAutoAim => _projectileAutoAim;
    public bool DestroyOnHit => _destroyOnHit;

    public bool HaveContinueAbility => _haveContinueAbility;
    public Ability ContinueAbility => _continueAbility;

    public bool HaveAura => _haveAura;
    public GameObject AuraObject => _auraObject;

    public float[] Damage => _damage;
    public float[] DamageMultiplier => _damageMultiplier;
    public float[] CastPerSecond => _castPerSecond;
    public float[] CritChance => _critChance;
    public float[] CritMultiplier => _critMultiplier;

    public string Name => _name;
    public string Description => _description;

    public float[] Radius => _radius;

    public AbilityProperty[] AbilityProperties => _abilityProperties;
}

[Serializable]
public class AbilityProperty
{
    [SerializeField] public string Name;
    [SerializeField] public float[] Values;
}