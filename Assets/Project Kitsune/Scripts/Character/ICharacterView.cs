using UnityEngine;

public interface ICharacterView
{
    public void SpawnCharacter(GameObject prefab);
    public void SetPosition(Vector2 position);
}