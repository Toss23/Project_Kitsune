using UnityEngine;

public interface ICharacterView
{
    public AbilityPoints AbilityPoints { get; }
    public bool IsReversed { get; }
    public float Angle { get; }

    public void Move(bool move);
    public void SetAngle(float angle);
    public void SpawnCharacter(GameObject prefab);
    public void SetPosition(Vector2 position);
}