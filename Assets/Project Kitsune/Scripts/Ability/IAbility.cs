using System;
using UnityEngine;

public interface IAbility
{
    public event Action<IAbility, IUnit> OnHit;

    public AbilityInfo Info { get; }
    public int Level { get; }
    public int MaxLevel { get; }

    public void FuseWith(Transform transform);
    public void Init(int level, bool castedByCharacter);
    public void Destroy();
}