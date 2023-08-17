using UnityEngine;

[CreateAssetMenu(fileName = "Base Ability Data", menuName = "Ability/Base Ability Data")]
public class BaseAbilityData : AbilityData
{
    [Header("Damage")]
    [SerializeField] private bool _useCharacterDamage;
    [SerializeField] private bool _useCharacterCrit;

    [SerializeField] private float[] _damage;
    [SerializeField] private float[] _damageMultiplier;
    [SerializeField] private float[] _castPerSecond;
    [SerializeField] private float[] _critChance;
    [SerializeField] private float[] _critMultiplier;
    [SerializeField] private float[] _dotRate;

    public bool UseCharacterDamage => _useCharacterDamage;
    public bool UseCharacterCrit => _useCharacterCrit;

    public float[] Damage => _damage;
    public float[] DamageMultiplier => _damageMultiplier;
    public float[] CastPerSecond => _castPerSecond;
    public float[] CritChance => _critChance;
    public float[] CritMultiplier => _critMultiplier;
    public float[] DotRate => _dotRate;

    public override int GetMaxLevel()
    {
        int maxLevel = base.GetMaxLevel();
        if (_damage != null & _damageMultiplier != null & _castPerSecond != null & _critChance != null & _critMultiplier != null & _dotRate != null)
        {
            maxLevel = Mathf.Max(maxLevel, _damage.Length, _damageMultiplier.Length, _castPerSecond.Length, _critChance.Length, _critMultiplier.Length, _dotRate.Length) - 1;
        }
        return maxLevel;
    }
}