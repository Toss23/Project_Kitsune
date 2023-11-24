using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Spawner Info", menuName = "Create Enemy Spawner Info")]
public class EnemySpawnerInfo : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private SpawnRule[] _spawnRules;

    public string Name => _name;
    public SpawnRule[] SpawnRules => _spawnRules;
}

[System.Serializable]
public class SpawnRule
{
    [SerializeField] private EnemyPresenter _enemy;
    [Space(10)]
    [SerializeField] private int _countStart;
    [SerializeField] private int _countEnd;
    [Space(10)]
    [SerializeField] private float _startTime;
    [SerializeField] private float _endTime;
    [Space(10)]
    [SerializeField] private float _loopTimeStart;
    [SerializeField] private float _loopTimeEnd;

    public EnemyPresenter Enemy => _enemy;
    public int CountStart => _countStart;
    public int CountEnd => _countEnd;

    public float StartTime => _startTime;
    public float EndTime => _endTime;

    public float LoopTimeStart => _loopTimeStart;
    public float LoopTimeEnd => _loopTimeEnd;
}