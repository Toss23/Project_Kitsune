public class ModifiersContainer
{
    private AttributesContainer _attributesContainer;
    private AbilityModifier[] _abilityModifiers;

    public AbilityModifier[] AbilityModifiers => _abilityModifiers;

    public ModifiersContainer(AttributesContainer attributesContainer)
    {
        _attributesContainer = attributesContainer;

        _abilityModifiers = new AbilityModifier[5];
        for (int i = 0; i < _abilityModifiers.Length; i++)
        {
            _abilityModifiers[i] = new AbilityModifier(i);
        }
    }

    public void Add(AbilityModifier abilityModifier)
    {
        _abilityModifiers[abilityModifier.AbilityIndex] = Sum(_abilityModifiers[abilityModifier.AbilityIndex], abilityModifier);
    }

    public void Remove(AbilityModifier abilityModifier)
    {
        _abilityModifiers[abilityModifier.AbilityIndex] = Difference(_abilityModifiers[abilityModifier.AbilityIndex], abilityModifier);
    }

    private AbilityModifier Sum(AbilityModifier first, AbilityModifier second)
    {
        first.Damage += second.Damage;
        first.Multiplier += second.Multiplier;
        first.CastPerSecond += second.CastPerSecond;
        first.CritChance += second.CritChance;
        first.CritMultiplier += second.CritMultiplier;

        first.ProjectileCount += second.ProjectileCount;
        first.ProjectileAngle += second.ProjectileAngle;

        first.DotRate += second.DotRate;
        first.DotDuration += second.DotDuration;

        first.Radius += second.Radius;

        return first;
    }

    private AbilityModifier Difference(AbilityModifier first, AbilityModifier second)
    {
        first.Damage -= second.Damage;
        first.Multiplier -= second.Multiplier;
        first.CastPerSecond -= second.CastPerSecond;
        first.CritChance -= second.CritChance;
        first.CritMultiplier -= second.CritMultiplier;

        first.ProjectileCount -= second.ProjectileCount;
        first.ProjectileAngle -= second.ProjectileAngle;

        first.DotRate -= second.DotRate;
        first.DotDuration -= second.DotDuration;

        first.Radius -= second.Radius;

        return first;
    }
}