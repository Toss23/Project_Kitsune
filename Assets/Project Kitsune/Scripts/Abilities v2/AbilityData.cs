using UnityEngine;

public class AbilityData : ScriptableObject
{
    [Header("Default")]
    [SerializeField] private string _name;
    [SerializeField] private string _description;

    [SerializeField] private bool _useCharacterDamage;
    [SerializeField] private bool _useCharacterCrit;

    [SerializeField] private float[] _damage;
    [SerializeField] private float[] _damageMultiplier;
    [SerializeField] private float[] _castPerSecond;
    [SerializeField] private float[] _critChance;
    [SerializeField] private float[] _critMultiplier;
    [SerializeField] private float[] _scale;

    public string Name => _name;
    public string Description => _description;

    public bool UseCharacterDamage => _useCharacterDamage;
    public bool UseCharacterCrit => _useCharacterCrit;

    public float[] Damage => _damage;
    public float[] DamageMultiplier => _damageMultiplier;
    public float[] CastPerSecond => _castPerSecond;
    public float[] CritChance => _critChance;
    public float[] CritMultiplier => _critMultiplier;
    public float[] Scale => _scale;
}