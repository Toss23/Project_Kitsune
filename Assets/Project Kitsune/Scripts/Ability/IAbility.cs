using System;

public interface IAbility
{
    public event Action<IAbility, IEnemy> OnHit;

    public AbilityInfo Info { get; }

    public void Destroy();
}