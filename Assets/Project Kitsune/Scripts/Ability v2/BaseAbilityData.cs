using UnityEngine;

[CreateAssetMenu(fileName = "Base Ability Data", menuName = "Ability/Base Ability Data")]
public class BaseAbilityData : AbilityData
{
    public enum DamageType
    {
        OnHitDamage, DamageOverTime
    }

    [Header("Damage")]
    [SerializeField] private bool _useCharacterDamage;
    [SerializeField] private bool _useCharacterCrit;

    [SerializeField] private DamageType _damageType;
    [SerializeField] private float[] _damage;
    [SerializeField] private float[] _damageMultiplier;
    [SerializeField] private float[] _cooldown;
    [SerializeField] private float[] _critChance;
    [SerializeField] private float[] _critMultiplier;

    public bool UseCharacterDamage => _useCharacterDamage;
    public bool UseCharacterCrit => _useCharacterCrit;

    public float[] Damage => _damage;
    public float[] DamageMultiplier => _damageMultiplier;
    public float[] Cooldown => _cooldown;
    public float[] CritChance => _critChance;
    public float[] CritMultiplier => _critMultiplier;
}