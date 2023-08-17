using System;
using System.Collections.Generic;

public interface IAbility2
{
    public event Action<IAbility2, Unit> OnHit;

    public AbilityData AbilityData { get; }
    public int Level { get; }
    public AbilityModifier AbilityModifier { get; }
    public Dictionary<string, float> Properties { get; }

    public void Init(int level, Unit caster, UnitType target, AbilityModifier abilityModifier);
    public void DestroyAbility();
}
