using UnityEngine;

[CreateAssetMenu(fileName = "New Unit Info", menuName = "Create Unit Info")]
public class UnitInfo : ScriptableObject
{
    [SerializeField] private GameObject _prefab;

    [SerializeField] private float _life = 100;
    [SerializeField] private float _lifeRegeneration = 0;
    [SerializeField] private float _magicShield = 0;
    [SerializeField] private float _magicShieldRegeneration = 0;
    [SerializeField] private float _damage = 1;
    [SerializeField] private float _critChance = 0;
    [SerializeField] private float _critMultiplier = 150;
    [SerializeField] private float _armour = 0;
    [SerializeField] private float _movespeed = 1;
    [SerializeField] private float _experienceGain = 0;

    [SerializeField] private float _animationAttackTime = 1f;
    [SerializeField] private float _animationTimeToAttack = 1f;

    [SerializeField] private Ability[] _abilities = new Ability[6];

    public GameObject Prefab => _prefab;
    public float Life => _life;
    public float LifeRegeneration => _lifeRegeneration;
    public float MagicShield => _magicShield;
    public float MagicShieldRegeneration => _magicShieldRegeneration;
    public float Damage => _damage;
    public float CritChance => _critChance;
    public float CritMultiplier => _critMultiplier;
    public float Armour => _armour;
    public float Movespeed => _movespeed;
    public float ExperienceGain => _experienceGain;
    public float AnimationAttackTime => _animationAttackTime;
    public float AnimationTimeToAttack => _animationTimeToAttack;
    public IAbility[] Abilities => _abilities;
}