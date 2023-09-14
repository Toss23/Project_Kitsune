using System.Collections.Generic;

public class FoxSpirit : Ability
{
    private AbilityModifier _abilityModifierAttack;
    private AbilityModifier _abilityModifierTornado;

    protected override void OnCreateAbility()
    {
        _abilityModifierAttack = new AbilityModifier(0)
        {
            ProjectileCount = (int)Properties["MS_Count"],
            ProjectileTiltAngle = Properties["MS_Angle"] * Properties["MS_Count"]
        };

        _abilityModifierTornado = new AbilityModifier(1)
        {
            Scale = Properties["T_Scale"]
        };

        _caster.ModifiersContainer.Add(_abilityModifierAttack);
        _caster.ModifiersContainer.Add(_abilityModifierTornado);
    }

    protected override void OnDestroyAbility()
    {
        _caster.ModifiersContainer.Remove(_abilityModifierAttack);
        _caster.ModifiersContainer.Remove(_abilityModifierTornado);
    }
}
