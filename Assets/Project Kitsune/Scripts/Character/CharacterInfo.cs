using UnityEngine;

[CreateAssetMenu(fileName = "New Character Info", menuName = "Create Character Info")]
public class CharacterInfo : ScriptableObject
{
    [SerializeField] private GameObject _prefab;

    [SerializeField] private float _life = 100;
    [SerializeField] private float _regeneration = 0;
    [SerializeField] private float _damage = 1;
    [SerializeField] private float _critChance = 0;
    [SerializeField] private float _critMultiplier = 150;
    [SerializeField] private float _armour = 0;

    [SerializeField] private Ability[] _abilities = new Ability[4];

    public GameObject Prefab { get { return _prefab; } }
    public float Life { get { return _life; } }
    public float Regeneration { get { return _regeneration; } }
    public float Damage { get { return _damage; } }
    public float CritChance { get { return _critChance; } }
    public float CritMultiplier { get { return _critMultiplier; } }
    public float Armour { get { return _armour; } }
    public Ability[] Abilities { get { return _abilities; } }
}