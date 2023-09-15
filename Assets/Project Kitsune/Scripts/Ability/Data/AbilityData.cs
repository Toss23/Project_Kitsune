using UnityEngine;

[CreateAssetMenu(fileName = "Passive Ability Data", menuName = "Ability/Passive Ability Data")]
public class AbilityData : ScriptableObject
{
    public enum Type
    {
        Passive, Base, Range
    }

    [Header("Default")]
    [SerializeField] private string _name;
    [SerializeField] private string _description;

    [SerializeField] private bool _rotateAroundCaster;
    [SerializeField] private bool _fuseWithCaster;
    [SerializeField] private bool _spawnOnNearestEnemy;
    [SerializeField] private float _spawnRange;

    [SerializeField] private float[] _scale;
    [SerializeField] private bool _haveDuration;
    [SerializeField] private float[] _duration;

    [SerializeField] private AbilityProperty[] _abilityProperties;

    public string Name => _name;
    public string Description => _description;

    public bool RotateAroundCaster => _rotateAroundCaster;
    public bool FuseWithCaster => _fuseWithCaster;
    public bool SpawnOnNearestEnemy => _spawnOnNearestEnemy;
    public float SpawnRange => _spawnRange;

    public ArrayData<float> Scale => new ArrayData<float>(_scale);
    public bool HaveDuration => _haveDuration;
    public ArrayData<float> Duration => new ArrayData<float>(_duration);

    public AbilityProperty[] AbilityProperties => _abilityProperties;

    public virtual int GetMaxLevel()
    {
        int maxLevel = 0;

        if (_scale != null & _duration != null)
        {
            maxLevel = Mathf.Max(_scale.Length, _duration.Length) - 1;

            if (_abilityProperties != null & _abilityProperties.Length > 0)
            {
                foreach (AbilityProperty abilityProperty in _abilityProperties)
                {
                    maxLevel = Mathf.Max(maxLevel, abilityProperty.Values.Length - 1);
                }
            }
        }
        return maxLevel;
    }

    public virtual Type GetAbilityType()
    {
        return Type.Passive;
    }
}