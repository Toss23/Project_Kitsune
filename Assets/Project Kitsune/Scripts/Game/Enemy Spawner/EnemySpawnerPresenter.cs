using UnityEngine;

public class EnemySpawnerPresenter : MonoBehaviour, IEnemySpawnerPresenter
{
    [SerializeField] private EnemySpawnerInfo _info;
    [SerializeField] private float _spawnRadius;

    private IContext _logic;
    private IUnitPresenter _characterPresenter;
    private EnemySpawner _enemySpawner;

    public void Init(IContext logic, IUnitPresenter character)
    {
        _logic = logic;
        _enemySpawner = new EnemySpawner(_info);
        _characterPresenter = character;
        Enable();
    }

    public void Enable()
    {
        _logic.OnUpdate += _enemySpawner.Update;
        _enemySpawner.SpawnEnemy += SpawnEnemy;
    }

    public void Disable()
    {
        _logic.OnUpdate -= _enemySpawner.Update;
        _enemySpawner.SpawnEnemy -= SpawnEnemy;
    }

    private void SpawnEnemy(IUnitPresenter unitPresenter)
    {
        float randomAngle = Random.Range(0, 360) * Mathf.Deg2Rad;
        Vector2 randomPosition = new Vector2();
        randomPosition.x = _spawnRadius * Mathf.Cos(randomAngle);
        randomPosition.y = _spawnRadius * Mathf.Sin(randomAngle);

        GameObject enemy = Instantiate(unitPresenter.Transform.gameObject, transform);
        enemy.transform.position = (Vector2)_characterPresenter.Transform.position + randomPosition;
        enemy.name = unitPresenter.ToString();

        IUnitPresenter enemyPresenter = enemy.GetComponent<UnitPresenter>();
        enemyPresenter.Init(_logic, UnitType.Enemy);
    }
}
