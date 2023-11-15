using System;
using System.Collections.Generic;

public interface IAbility
{
    public event Action<IAbility, Unit> OnHit;

    public AbilityData AbilityData { get; }
    public int Level { get; }
    public AbilityModifier AbilityModifier { get; }
    public Dictionary<string, float> Properties { get; }

    public void Init(IContext logic, int abilityIndex, int level, Unit caster, UnitType target, AbilityModifier abilityModifier);
    public void DestroyAbility();
}
