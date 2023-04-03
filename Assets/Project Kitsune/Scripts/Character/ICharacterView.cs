using UnityEngine;

public interface ICharacterView
{
    public void Move(bool move);
    public void Reverse(float angle);
    public void SpawnCharacter(GameObject prefab);
    public void SetPosition(Vector2 position);
}