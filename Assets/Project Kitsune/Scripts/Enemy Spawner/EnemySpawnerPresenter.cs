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
        Enable();
    }

    private void Update()
    {
        _enemySpawner.Update(Time.deltaTime);
    }

    private void Enable()
    {
        _enemySpawner.SpawnEnemy += SpawnEnemy;
        _characterPresenter.OnFreeze += _enemySpawner.Freeze;
    }

    private void Disable()
    {
        _enemySpawner.SpawnEnemy -= SpawnEnemy;
        _characterPresenter.OnFreeze -= _enemySpawner.Freeze;
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
