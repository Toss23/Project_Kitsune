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
    [SerializeField] private int[] _projectileCount;
    [SerializeField] private float[] _projectileSplitAngle;
    [SerializeField] private bool _projectileAuto;
    [SerializeField] private bool _destroyOnHit;

    [SerializeField] private bool _haveContinueAbility;
    [SerializeField] private Ability _continueAbility;

    [SerializeField] private float[] _damage;
    [SerializeField] private float[] _damageMultiplier;

    [SerializeField] private float[] _castPerSecond;

    [SerializeField] private float[] _critChance;
    [SerializeField] private float[] _critMultiplier;

    [SerializeField] private string _description;

    [SerializeField] private float[] _radius;

    public bool UseCharacterDamage => _useCharacterDamage;
    public bool UseCharacterCrit => _useCharacterCrit;

    public DamageType AbilityDamageType => _abilityDamageType;
    public float[] DotRate => _dotRate;
    public float[] DotDuration => _dotDuration;

    public Type AbilityType => _abilityType;
    public float MeleeAnimationTime => _meleeAnimationTime;
    public float ProjectileSpeed => _projectileSpeed;
    public float ProjectileRange => _projectileRange;
    public int[] ProjectileCount => _projectileCount;
    public float[] ProjectileSpliteAngle => _projectileSplitAngle;
    public bool ProjectileAuto => _projectileAuto;
    public bool DestroyOnHit => _destroyOnHit;

    public bool HaveContinueAbility => _haveContinueAbility;
    public Ability ContinueAbility => _continueAbility;

    public float[] Damage => _damage;
    public float[] DamageMultiplier => _damageMultiplier;
    public float[] CastPerSecond => _castPerSecond;
    public float[] CritChance => _critChance;
    public float[] CritMultiplier => _critMultiplier;

    public string Description => _description;

    public float[] Radius => _radius;
}