public class FoxSpirit : Ability
{
    private AbilityModifier _abilityModifierAttack;

    protected override void OnCollisionEnterWithEnemy(IUnit caster, IUnit target)
    {
        
    }

    protected override void OnCollisionStayWithEnemy(IUnit caster, IUnit target)
    {
        
    }

    protected override void OnCreateAbility(IUnit caster)
    {
        _abilityModifierAttack = new AbilityModifier(0)
        {
            ProjectileCount = 2,
            ProjectileAngle = 60
        };

        caster.ModifiersContainer.Add(_abilityModifierAttack);
    }

    protected override void OnUpdateAbility(IUnit caster, float deltaTime)
    {
        
    }

    protected override void OnDestroyAbility(IUnit caster)
    {
        caster.ModifiersContainer.Remove(_abilityModifierAttack);
    }
}
