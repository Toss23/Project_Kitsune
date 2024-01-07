public class Kitsune : Ability
{
    private AbilityModifier _abilityModifierMagicSphere;
    private AbilityModifier _abilityModifierVines;
    private AbilityModifier _abilityFoxSpirit;
    private AbilityModifier _abilityVortex;

    protected override void OnCreateAbility()
    {
        _abilityModifierMagicSphere = new AbilityModifier(0)
        {
            ProjectileCount = (int)Properties["MS_Count"],
            ProjectileTiltAngle = 20
        };

        _abilityModifierVines = new AbilityModifier(1)
        {
            Damage = Properties["V_Damage"]
        };

        _abilityFoxSpirit = new AbilityModifier(2)
        {
            Damage = Properties["FS_Damage"]
        };

        _abilityVortex = new AbilityModifier(3)
        {
            DotRate = Properties["Vx_Rate"],
            Scale = Properties["Vx_Scale"]
        };

        _caster.ModifiersContainer.Add(_abilityModifierMagicSphere);
        _caster.ModifiersContainer.Add(_abilityModifierVines);
        _caster.ModifiersContainer.Add(_abilityFoxSpirit);
        _caster.ModifiersContainer.Add(_abilityVortex);

        _caster.Abilities.UpdatePassiveAbility(3);
    }

    protected override void OnDestroyAbility()
    {
        _caster.ModifiersContainer.Remove(_abilityModifierMagicSphere);
        _caster.ModifiersContainer.Remove(_abilityModifierVines);
        _caster.ModifiersContainer.Remove(_abilityFoxSpirit);
        _caster.ModifiersContainer.Remove(_abilityVortex);
    }
}
