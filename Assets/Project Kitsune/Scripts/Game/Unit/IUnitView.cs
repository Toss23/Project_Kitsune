using UnityEngine;

public interface IUnitView
{
    public IAbilityPoints AbilityPoints { get; }
    public float Angle { get; }

    public void CreateUnit(GameObject prefab, float defaultActionSpeed);
    public void SetMovingAndAngle(bool moving, float angle);
    public void SetActive(bool state);
    public void SetAttacking(bool active);
    public void SetActionSpeed(float speed);
    public void SetCurseIcon(Curse curse, bool active);
    public void SetMagicShield(bool active);
}