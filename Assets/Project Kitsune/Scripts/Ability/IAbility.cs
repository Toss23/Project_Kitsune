using System;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    public event Action<IAbility, IUnit> OnHit;

    public AbilityInfo Info { get; }
    public Dictionary<string, float> Properties { get; }
    public int Level { get; }
    public int MaxLevel { get; }
    public AbilityModifier AbilityModifier { get; }

    public void FuseWith(Transform transform);
    public void Init(int level, IUnit caster, UnitType target, AbilityModifier abilityModifier);
    public void Destroy();
}