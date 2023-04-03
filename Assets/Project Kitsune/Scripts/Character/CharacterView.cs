using UnityEngine;

public class CharacterView : MonoBehaviour, ICharacterView
{
    [SerializeField] private Transform _delta;

    private GameObject _character;
    private Animator _animator;

    public void Move(bool move)
    {
        _animator.SetBool("Move", move);
    }

    public void Reverse(float angle)
    {
        transform.rotation = Quaternion.Euler(0, Mathf.Abs(angle) > 90 ? 180 : 0, 0);
    }

    public void SpawnCharacter(GameObject prefab)
    {
        _character = Instantiate(prefab, _delta);
        _character.name = prefab.name;
        _animator = _character.GetComponent<Animator>();
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }
}