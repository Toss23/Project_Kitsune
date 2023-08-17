public class FoxSpirit : Ability
{
    private AbilityModifier _abilityModifierAttack;

    protected override void OnCreateAbility()
    {
        _abilityModifierAttack = new AbilityModifier(0)
        {
            ProjectileCount = 2,
            ProjectileAngle = 60
        };

        _caster.ModifiersContainer.Add(_abilityModifierAttack);
    }

    protected override void OnDestroyAbility()
    {
        _caster.ModifiersContainer.Remove(_abilityModifierAttack);
    }
}
