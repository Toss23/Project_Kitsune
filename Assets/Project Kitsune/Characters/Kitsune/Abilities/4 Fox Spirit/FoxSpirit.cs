public class FoxSpirit : Ability
{
    private AbilityModifier _abilityModifierAttack;

    protected override void OnCollisionEnterWithEnemy(Unit caster, Unit target)
    {
        
    }

    protected override void OnCollisionStayWithEnemy(Unit caster, Unit target)
    {
        
    }

    protected override void OnCreateAbility(Unit caster)
    {
        _abilityModifierAttack = new AbilityModifier(0)
        {
            ProjectileCount = 2,
            ProjectileAngle = 60
        };

        caster.ModifiersContainer.Add(_abilityModifierAttack);
    }

    protected override void OnUpdateAbility(Unit caster, float deltaTime)
    {
        
    }

    protected override void OnDestroyAbility(Unit caster)
    {
        caster.ModifiersContainer.Remove(_abilityModifierAttack);
    }
}
