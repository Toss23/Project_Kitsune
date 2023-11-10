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
            ProjectileCount = (int)Properties["MS_Count"]
        };

        _abilityModifierVines = new AbilityModifier(0)
        {
            Scale = (int)Properties["V_Scale"]
        };

        _abilityFoxSpirit = new AbilityModifier(0)
        {
            Damage = (int)Properties["FS_Damage"]
        };

        _abilityVortex = new AbilityModifier(0)
        {
            DotRate = (int)Properties["V_Rate"]
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
