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
        Melee, Projectile
    }

    [SerializeField] private bool _useCharacterDamage = true;
    [SerializeField] private bool _useCharacterCrit = false;

    [SerializeField] private DamageType _abilityDamageType = DamageType.Hit;
    [SerializeField] private Type _abilityType = Type.Melee;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _projectileRange;

    [SerializeField] private float[] _damage;
    [SerializeField] private float[] _damageMultiplier;

    [SerializeField] private float[] _castPerSecond;

    [SerializeField] private float[] _critChance;
    [SerializeField] private float[] _critMultiplier;

    [SerializeField] private string _description;

    public bool UseCharacterDamage => _useCharacterDamage;
    public bool UseCharacterCrit => _useCharacterCrit;
    public DamageType AbilityDamageType => _abilityDamageType;
    public Type AbilityType => _abilityType;
    public float ProjectileSpeed => _projectileSpeed;
    public float ProjectileRange => _projectileRange;
    public float[] Damage => _damage;
    public float[] DamageMultiplier => _damageMultiplier;
    public float[] CastPerSecond => _castPerSecond;
    public float[] CritChance => _critChance;
    public float[] CritMultiplier => _critMultiplier;

    public string Description => _description;
}