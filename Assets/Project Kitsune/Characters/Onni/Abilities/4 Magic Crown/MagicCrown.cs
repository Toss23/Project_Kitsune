using UnityEngine;

public class MagicCrown : Ability
{
    protected override void OnCollisionEnterWithEnemy(IUnit enemy)
    {

    }

    protected override void OnCollisionStayWithEnemy(IUnit enemy)
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

    protected override void OnCreate()
    {
        
    }

    protected override void OnUpdate(float deltaTime)
    {
        
    }
}