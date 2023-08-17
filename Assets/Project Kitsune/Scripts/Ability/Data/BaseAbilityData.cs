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

    public ArrayData<float> Damage => new ArrayData<float>(_damage);
    public ArrayData<float> DamageMultiplier => new ArrayData<float>(_damageMultiplier);
    public ArrayData<float> CastPerSecond => new ArrayData<float>(_castPerSecond);
    public ArrayData<float> CritChance => new ArrayData<float>(_critChance);
    public ArrayData<float> CritMultiplier => new ArrayData<float>(_critMultiplier);
    public ArrayData<float> DotRate => new ArrayData<float>(_dotRate);

    public override int GetMaxLevel()
    {
        int maxLevel = base.GetMaxLevel() + 1;
        if (_damage != null & _damageMultiplier != null & _castPerSecond != null & _critChance != null & _critMultiplier != null & _dotRate != null)
        {
            maxLevel = Mathf.Max(maxLevel, _damage.Length, _damageMultiplier.Length, _castPerSecond.Length, _critChance.Length, _critMultiplier.Length, _dotRate.Length) - 1;
        }
        return maxLevel;
    }

    public override Type GetAbilityType()
    {
        return Type.Base;
    }
}