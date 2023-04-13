using System;

public class AbilitiesSelection
{
    public event Action<IAbility[]> onMethod;
    public event Action<IAbility> OnSelectedAbility;

    private IUnit _unit;

    public AbilitiesSelection(IUnit unit)
    {
        _unit = unit;
    }

    public void Method(float value)
    {
        IAbility[] abilities = new IAbility[2];
        abilities[0] = _unit.Abilities.List[0];
        abilities[1] = _unit.Abilities.List[0];
        onMethod?.Invoke(abilities);
    }

    public void OnSelected(IAbility ability)
    {
        OnSelectedAbility?.Invoke(ability);
    }
}