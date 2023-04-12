using UnityEngine;

public interface IUnitView
{
    public AbilityPoints AbilityPoints { get; }

    public float Angle { get; }

    public void CreateUnit(GameObject prefab);  
    public void IsMoving(bool active);
    public void SetPosition(Vector2 position);
    public void SetAngle(float angle);
    public void SetPositionAndAngle(Vector2 position, float angle);
}