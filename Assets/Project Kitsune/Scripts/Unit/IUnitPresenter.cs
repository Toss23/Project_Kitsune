using UnityEngine;

public enum UnitType
{
    Character, Enemy
}

public interface IUnitPresenter
{
    public IUnit Unit { get; }
    public IUnitView UnitView { get; }
    public Transform Transform { get; }

    public void Init(UnitType unitType);
    public void Enable();
    public void Disable();
}