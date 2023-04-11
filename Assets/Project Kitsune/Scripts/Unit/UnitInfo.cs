using UnityEngine;

[CreateAssetMenu(fileName = "New Unit Info", menuName = "Create Unit Info")]
public class UnitInfo : ScriptableObject
{
    [SerializeField] private GameObject _prefab;

    [SerializeField] private float _life = 100;
    [SerializeField] private float _regeneration = 0;
    [SerializeField] private float _damage = 1;
    [SerializeField] private float _critChance = 0;
    [SerializeField] private float _critMultiplier = 150;
    [SerializeField] private float _armour = 0;

    [SerializeField] private Ability[] _abilities = new Ability[5];

    public GameObject Prefab => _prefab;
    public float Life => _life;
    public float Regeneration => _regeneration;
    public float Damage => _damage;
    public float CritChance => _critChance;
    public float CritMultiplier => _critMultiplier;
    public float Armour => _armour;
    public IAbility[] Abilities => _abilities;
}