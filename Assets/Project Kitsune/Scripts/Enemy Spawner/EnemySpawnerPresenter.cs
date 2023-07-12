using UnityEngine;

public class EnemySpawnerPresenter : MonoBehaviour
{
    [SerializeField] private EnemySpawnerInfo _info;
    [SerializeField] private CharacterPresenter _characterPresenter;
    [SerializeField] private float _spawnRadius;

    private EnemySpawner _enemySpawner;

    private void Awake()
    {
        _enemySpawner = new EnemySpawner(_info);
        GameLogic.Instance.OnUpdate += _enemySpawner.Update;
        Enable();
    }

    private void Enable()
    {
        _enemySpawner.SpawnEnemy += SpawnEnemy;
    }

    private void Disable()
    {
        _enemySpawner.SpawnEnemy -= SpawnEnemy;
    }

    private void SpawnEnemy(IUnitPresenter unitPresenter)
    {
        GameObject enemy = Instantiate(unitPresenter.Transform.gameObject, transform);

        float randomAngle = Random.Range(0, 360) * Mathf.Deg2Rad;

        Vector2 randomPosition = new Vector2();
        randomPosition.x = _spawnRadius * Mathf.Cos(randomAngle);
        randomPosition.y = _spawnRadius * Mathf.Sin(randomAngle);

        enemy.transform.position = (Vector2)_characterPresenter.Transform.position + randomPosition;
        enemy.name = unitPresenter.ToString();
    }
}
