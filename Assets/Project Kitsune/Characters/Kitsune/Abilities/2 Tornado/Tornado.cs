using UnityEngine;

public class Tornado : Ability
{
    protected override void OnCollisionEnterWithEnemy(IUnit caster, IUnit target)
    {
        
    }

    protected override void OnCollisionStayWithEnemy(IUnit caster, IUnit target)
    {
        
    }

    protected override void OnCreate()
    {
        Transform nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null)
        {
            transform.position = nearestEnemy.position;
        }
    }

    protected override void OnUpdate(float deltaTime)
    {
        
    }
}