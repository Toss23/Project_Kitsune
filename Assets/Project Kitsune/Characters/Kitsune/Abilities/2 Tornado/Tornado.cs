using UnityEngine;

public class Tornado : Ability
{
    protected override void OnCollisionEnterWithEnemy(Unit caster, Unit target)
    {
        
    }

    protected override void OnCollisionStayWithEnemy(Unit caster, Unit target)
    {
        
    }

    protected override void OnCreateAbility(Unit caster)
    {
        Transform nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null)
        {
            transform.position = nearestEnemy.position;
        }
    }

    protected override void OnUpdateAbility(Unit caster, float deltaTime)
    {
        
    }

    protected override void OnDestroyAbility(Unit caster)
    {

    }
}