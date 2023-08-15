using UnityEngine;

public class MagicCrown : Ability
{
    protected override void OnCollisionEnterWithEnemy(Unit caster, Unit target)
    {
        
    }

    protected override void OnCollisionStayWithEnemy(Unit caster, Unit target)
    {
        
    }

    protected override void OnCollisionWithObject(GameObject gameObject)
    {
        if (gameObject.tag == "Projectile")
        {
            IAbility ability = gameObject.GetComponent<Ability>();
            if (ability != null)
            {
                ability.Destroy();
            }
        }
    }

    protected override void OnCreateAbility(Unit caster)
    {
        
    }

    protected override void OnUpdateAbility(Unit caster, float deltaTime)
    {
        
    }

    protected override void OnDestroyAbility(Unit caster)
    {

    }
}