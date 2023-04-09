using UnityEngine;

public class CharacterView : MonoBehaviour, ICharacterView
{
    [SerializeField] private Transform _delta;

    private GameObject _character;
    private Animator _animator;
    private AbilityPoints _abilityPoints;
    private bool _isReversed;
    private float _angle;

    public AbilityPoints AbilityPoints => _abilityPoints;
    public bool IsReversed => _isReversed;
    public float Angle => _angle;

    public void Move(bool move)
    {
        _animator.SetBool("Move", move);
    }

    public void SetAngle(float angle)
    {
        _angle = angle;
        _isReversed = Mathf.Abs(angle) > 90;
        transform.rotation = Quaternion.Euler(0, _isReversed ? 180 : 0, 0);
    }

    public void SpawnCharacter(GameObject prefab)
    {
        _character = Instantiate(prefab, _delta);
        _character.name = prefab.name;
        _animator = _character.GetComponent<Animator>();
        _abilityPoints = _character.GetComponent<AbilityPoints>();
        _isReversed = false;
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }
}