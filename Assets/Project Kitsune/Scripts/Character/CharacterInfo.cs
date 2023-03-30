using UnityEngine;

[CreateAssetMenu(fileName = "New Character Info", menuName = "Create Character Info")]
public class CharacterInfo : ScriptableObject
{
    [Header("Object")]
    [SerializeField] private GameObject _prefab;

    [Header("Attributes")]
    [SerializeField] private float _life = 100;
    [SerializeField] private float _regeneration = 0;
    [SerializeField] private float _damage = 1;
    [SerializeField] private float _critChance = 0;
    [SerializeField] private float _critMultiplier = 50;
    [SerializeField] private float _armour = 0;

    public GameObject Prefab { get { return _prefab; } }
    public float Life { get { return _life; } }
    public float Regeneration { get { return _regeneration; } }
    public float Damage { get { return _damage; } }
    public float CritChance { get { return _critChance; } }
    public float CritMultiplier { get { return _critMultiplier; } }
    public float Armour { get { return _armour; } }
}