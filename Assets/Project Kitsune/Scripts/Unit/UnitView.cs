using UnityEngine;

public abstract class UnitView : MonoBehaviour, IUnitView
{
    [SerializeField] private Transform _delta;

    protected GameObject _unit;
    protected Animator _animator;
    protected AbilityPoints _abilityPoints;
    protected bool _isReversed;
    protected float _angle;

    public AbilityPoints AbilityPoints => _abilityPoints;
    public bool IsReversed => _isReversed;
    public float Angle => _angle;

    public void IsMoving(bool move)
    {
        _animator.SetBool("Move", move);
    }

    public void SetAngle(float angle)
    {
        _angle = angle;
        _isReversed = Mathf.Abs(angle) > 90;
        transform.rotation = Quaternion.Euler(0, _isReversed ? 180 : 0, 0);
    }

    public void CreateUnit(GameObject prefab)
    {
        _unit = Instantiate(prefab, _delta);
        _unit.name = prefab.name;
        _animator = _unit.GetComponent<Animator>();
        _abilityPoints = _unit.GetComponent<AbilityPoints>();
        _isReversed = false;
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }
}