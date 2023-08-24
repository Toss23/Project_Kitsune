using System;

public interface IAbilityCardView
{
    public event Action<IAbility> OnClick;

    public void SetAbility(IAbility ability, int level);
}