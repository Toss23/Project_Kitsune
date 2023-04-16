using UnityEngine;

public interface IUnitPresenter
{
    public IUnit Unit { get; }
    public IUnitView UnitView { get; }
    public Transform Transform { get; }
}