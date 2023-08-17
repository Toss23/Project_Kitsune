using UnityEngine;

public abstract class HitAbility : Ability
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = HitCollisionEnemy(collision);
        if (unit != null)
        {
            OnHitEnemy(unit);

            if (_rangeAbilityData != null)
            {
                if (_rangeAbilityData.DestroyOnHit)
                {
                    DestroyAbility();
                }
            }
        }
        else
        {
            OnCollisionWithGameObject(collision.gameObject);
        }
    }

    protected virtual void OnHitEnemy(Unit enemy) { }
    protected virtual void OnCollisionWithGameObject(GameObject gameObject) { }
}
