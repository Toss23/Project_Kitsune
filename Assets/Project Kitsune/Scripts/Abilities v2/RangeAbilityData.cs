using UnityEngine;

[CreateAssetMenu(fileName = "Range Ability Data", menuName = "Ability/Range Ability Data")]
public class RangeAbilityData : AbilityData
{
    [Header("Range")]
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _spawnOffset;

    [SerializeField] private int[] _count;
    [SerializeField] private float[] _tiltAngle;

    [SerializeField] private bool _aimNearEnemy;
    [SerializeField] private bool _followsTarget;
    [SerializeField] private bool _destroyOnHit;

    public float Speed => _speed;
    public float LifeTime => _lifeTime;
    public float SpawnOffset => _spawnOffset;

    public int[] Count => _count;
    public float[] TiltAngle => _tiltAngle;

    public bool AimNearEnemy => _aimNearEnemy;
    public bool FollowsTarget => _followsTarget;
    public bool DestroyOnHit => _destroyOnHit;
}