using UnityEngine;

public class EnemyPresenter : MonoBehaviour
{
    public IEnemy Enemy { get; private set; }

    private void Awake()
    {
        Enemy = new Enemy();
    }
}