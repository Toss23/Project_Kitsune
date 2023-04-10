using System;
using UnityEngine;

public interface IAbility
{
    public event Action<IAbility, IEnemy> OnHit;

    public AbilityInfo Info { get; }
    public int Level { get; }
    public int MaxLevel { get; }

    public void InitPrefab();
    public void FuseWith(Transform transform);
    public void Init(int level);
    public void Destroy();
}