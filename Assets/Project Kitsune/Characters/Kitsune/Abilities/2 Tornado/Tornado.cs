using UnityEngine;

public class Tornado : Ability
{
    protected override void OnCollisionEnterWithEnemy(IUnit caster, IUnit target)
    {
        
    }

    protected override void OnCollisionStayWithEnemy(IUnit caster, IUnit target)
    {
        
    }

    protected override void OnCreateAbility(IUnit caster)
    {
        Transform nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null)
        {
            transform.position = nearestEnemy.position;
        }
    }

    protected override void OnUpdateAbility(IUnit caster, float deltaTime)
    {
        
    }

    protected override void OnDestroyAbility(IUnit caster)
    {

    }
}