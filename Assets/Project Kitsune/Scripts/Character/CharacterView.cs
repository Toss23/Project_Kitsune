using System;
using UnityEngine;

public class CharacterView : MonoBehaviour, ICharacterView
{
    public void SpawnCharacter(GameObject prefab)
    {

    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }
}