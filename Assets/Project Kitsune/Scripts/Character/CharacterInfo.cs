using UnityEngine;

[CreateAssetMenu(fileName = "New Character Info", menuName = "Create Character Info")]
public class CharacterInfo : ScriptableObject
{
    [SerializeField] private GameObject _prefab;

    public GameObject Prefab { get { return _prefab; } }
}