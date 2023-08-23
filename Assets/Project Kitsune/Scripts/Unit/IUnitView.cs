using UnityEngine;

public interface IUnitView
{
    public IAbilityPoints AbilityPoints { get; }
    public float Angle { get; }

    public void CreateUnit(GameObject prefab);
    public void IsMoving(bool active);
    public void SetAngle(float angle);
    public void SetActive(bool state);
    public void SetActionSpeed(float speed);
    public void SetCurseIcon(Curse curse, bool active);
    public void SetMagicShield(bool active);
}