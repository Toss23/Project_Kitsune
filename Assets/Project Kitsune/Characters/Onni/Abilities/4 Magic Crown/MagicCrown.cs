using UnityEngine;

public class MagicCrown : HitAbility
{
    protected override void OnCollisionWithGameObject(GameObject gameObject)
    {
        base.OnCollisionWithGameObject(gameObject);

        if (gameObject.CompareTag("Projectile"))
        {
            IAbility ability = gameObject.GetComponent<Ability>();
            if (ability != null)
            {
                ability.DestroyAbility();
            }
        }
    }
}