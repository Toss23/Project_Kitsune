using UnityEngine;

public interface IUnitView
{
    public AbilityPoints AbilityPoints { get; }

    public float Angle { get; }

    public void CreateUnit(GameObject prefab);  
    public void IsMoving(bool active);
    public void SetAngle(float angle);
    public void Freeze(bool state);
    public void SetAttackAnimationTime(float time);
}