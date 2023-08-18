using UnityEngine;

public class DotAbility : Ability
{
    private BaseAbilityData _baseAbilityData;
    private float _dotTimer = 0;

    protected override void OnCreateAbility() 
    {
        if (AbilityData.GetType() == typeof(BaseAbilityData)) 
        {
            _baseAbilityData = (BaseAbilityData)AbilityData;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_baseAbilityData != null)
        {
            if (_gameLogic.Paused == false)
            {
                _dotTimer += Time.deltaTime;

                float dotRate = Mathf.Max(_baseAbilityData.DotRate.Get(_level) + _abilityModifier.DotRate, 0.1f);

                while (_dotTimer >= dotRate)
                {
                    _dotTimer -= dotRate;

                    Unit unit = HitCollisionEnemy(collision);
                    if (unit != null)
                    {
                        OnHitEnemy(unit);
                    }
                    else
                    {
                        OnCollisionWithGameObject(collision.gameObject);
                    }
                }
            }
        }
    }

    protected virtual void OnHitEnemy(Unit enemy) { }
    protected virtual void OnCollisionWithGameObject(GameObject gameObject) { }
}
