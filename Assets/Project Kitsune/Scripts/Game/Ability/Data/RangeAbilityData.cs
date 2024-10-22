using UnityEngine;

[CreateAssetMenu(fileName = "Range Ability Data", menuName = "Ability/Range Ability Data")]
public class RangeAbilityData : BaseAbilityData
{
    [Header("Range")]
    [SerializeField] private float _speed;
    [SerializeField] private float _spawnOffset;

    [SerializeField] private int[] _count;
    [SerializeField] private float[] _tiltAngle;

    [SerializeField] private bool _aimNearestEnemy;
    [SerializeField] private bool _followNearestEnemy;
    [SerializeField] private bool _destroyOnHit;

    public float Speed => _speed;
    public float SpawnOffset => _spawnOffset;

    public ArrayData<int> Count => new ArrayData<int>(_count);
    public ArrayData<float> TiltAngle => new ArrayData<float>(_tiltAngle);

    public bool AimNearestEnemy => _aimNearestEnemy;
    public bool FollowNearestEnemy => _followNearestEnemy;
    public bool DestroyOnHit => _destroyOnHit;

    public override int GetMaxLevel()
    {
        int maxLevel = base.GetMaxLevel() + 1;
        if (_count != null & _tiltAngle != null)
        {
            maxLevel = Mathf.Max(maxLevel, _count.Length, _tiltAngle.Length) - 1;
        }
        return maxLevel;
    }

    public override Type GetAbilityType()
    {
        return Type.Range;
    }
}