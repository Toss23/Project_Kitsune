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
            Damage = (int)Properties["V_Damage"]
        };

        _abilityFoxSpirit = new AbilityModifier(2)
        {
            Damage = (int)Properties["FS_Damage"]
        };

        _abilityVortex = new AbilityModifier(3)
        {
            DotRate = (int)Properties["Vx_Rate"],
            Scale = (int)Properties["Vx_Scale"]
        };

        _caster.ModifiersContainer.Add(_abilityModifierMagicSphere);
        _caster.ModifiersContainer.Add(_abilityModifierVines);
        _caster.ModifiersContainer.Add(_abilityFoxSpirit);
        _caster.ModifiersContainer.Add(_abilityVortex);
    }

    protected override void OnDestroyAbility()
    {
        _caster.ModifiersContainer.Remove(_abilityModifierMagicSphere);
        _caster.ModifiersContainer.Remove(_abilityModifierVines);
        _caster.ModifiersContainer.Remove(_abilityFoxSpirit);
        _caster.ModifiersContainer.Remove(_abilityVortex);
    }
}
