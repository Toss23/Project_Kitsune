using UnityEngine;

public enum UnitType
{
    Character, Enemy
}

public interface IUnitPresenter
{
    public Unit Unit { get; }
    public IUnitView UnitView { get; }
    public UnitType UnitType { get; }
    public Transform Transform { get; }

    public void Init(IContext context, UnitType unitType);
    public void Enable();
    public void Disable();
}